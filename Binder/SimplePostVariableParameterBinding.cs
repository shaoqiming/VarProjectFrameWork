﻿using exin.FrameWork.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using VarProject.FrameWork.Core.Utils;


public class SimplePostVariableParameterBinding : HttpParameterBinding
{
    public const string MultipleBodyParameters = "MultipleBodyParameters";
    public const string MultipleBodyParametersRaw = "MultipleBodyParametersRaw";
    public const bool AllowJsonContentType = true;


    public SimplePostVariableParameterBinding(HttpParameterDescriptor descriptor)
        : base(descriptor)
    {
    }

    /// <summary>
    /// Check for simple binding parameters in POST data. Bind POST
    /// data as well as query string data
    /// </summary>
    /// <param name="metadataProvider"></param>
    /// <param name="actionContext"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
                                                HttpActionContext actionContext,
                                                CancellationToken cancellationToken)
    {
        string stringValue = null;

        NameValueCollection col = TryReadBody(actionContext.Request);
        if (col != null)
            stringValue = col[Descriptor.ParameterName];

        // try reading query string if we have no POST/PUT match
        if (stringValue == null)
        {
            var query = actionContext.Request.GetQueryNameValuePairs();
            if (query != null)
            {
                var matches = query.Where(kv => kv.Key.ToLower() == Descriptor.ParameterName.ToLower());
                var keyValuePairs = matches as IList<KeyValuePair<string, string>> ?? matches.ToList();
                if (keyValuePairs.Any())
                    stringValue = keyValuePairs.First().Value;
            }
        }

        object value = StringToType(stringValue);

        // Set the binding result here
        SetValue(actionContext, value);

        // now, we can return a completed task with no result
        TaskCompletionSource<AsyncVoid> tcs = new TaskCompletionSource<AsyncVoid>();
        tcs.SetResult(default(AsyncVoid));
        return tcs.Task;
    }


    /// <summary>
    /// Method that implements parameter binding hookup to the global configuration object's
    /// ParameterBindingRules collection delegate.
    /// 
    /// This routine filters based on POST/PUT method status and simple parameter
    /// types.
    /// </summary>
    /// <example>
    /// GlobalConfiguration.Configuration.
    ///       .ParameterBindingRules
    ///       .Insert(0,SimplePostVariableParameterBinding.HookupParameterBinding);
    /// </example>    
    /// <param name="descriptor"></param>
    /// <returns></returns>
    public static HttpParameterBinding HookupParameterBinding(HttpParameterDescriptor descriptor)
    {
        //To see is it mark the flag
        if (descriptor.ActionDescriptor.GetCustomAttributes<System.Web.Http.MultiParameterSupportAttribute>().Count <= 0)
            return null;

        var supportedMethods = descriptor.ActionDescriptor.SupportedHttpMethods;

        // Only apply this binder on POST and PUT operations
        if (supportedMethods.Contains(HttpMethod.Post) ||
            supportedMethods.Contains(HttpMethod.Put))
        {
            var paraType = descriptor.ParameterType;
            var supportedTypes = new Type[] { typeof(string),
                                                typeof(int),
                                                typeof(int?),
                                                typeof(decimal),
                                                typeof(decimal?),
                                                typeof(double),
                                                typeof(double?),
                                                typeof(long),
                                                typeof(long?),
                                                typeof(bool),
                                                typeof(bool?),
                                                typeof(DateTime),
                                                typeof(DateTime?),
                                                typeof(byte[]),
                                                typeof(Guid),
                                                typeof(Guid?),
                                            };

            Func<Type, bool> isSupport = (t) =>
            {
                return supportedTypes.Count(typ => typ == t) > 0 || paraType.IsEnum || IsNullableEnum(t);
            };

            if (paraType.IsArray)
            {
                var eleType = paraType.GetElementType();
                if (eleType != typeof(byte))// if (isSupport(eleType))
                {
                    return new SimplePostVariableParameterBinding(descriptor);
                }
            }

            if (isSupport(paraType))
                return new SimplePostVariableParameterBinding(descriptor);
        }

        return null;
    }

    /// <summary>
    /// 是否为可空类型的枚举
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private static bool IsNullableEnum(Type t)
    {
        Type u = Nullable.GetUnderlyingType(t);
        return (u != null) && u.IsEnum;
    }

    object StringToType(string stringValue)
    {
        object value = null;

        if (stringValue == null) value = null;
        else if (Descriptor.ParameterType == typeof(string)) value = stringValue;
        else if (Descriptor.ParameterType == typeof(int)) value = int.Parse(stringValue, CultureInfo.CurrentCulture);

        else if (Descriptor.ParameterType == typeof(int?)) value = string.IsNullOrWhiteSpace(stringValue) ? (int?)null : int.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(long)) value = long.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(long?)) value = string.IsNullOrWhiteSpace(stringValue) ? (long?)null : long.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(decimal)) value = decimal.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(decimal?)) value = string.IsNullOrWhiteSpace(stringValue) ? (decimal?)null : decimal.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(double)) value = double.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(double?)) value = string.IsNullOrWhiteSpace(stringValue) ? (double?)null : double.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(DateTime)) value = DateTime.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(DateTime?)) value = string.IsNullOrWhiteSpace(stringValue) ? (DateTime?)null : DateTime.Parse(stringValue, CultureInfo.CurrentCulture);
        else if (Descriptor.ParameterType == typeof(Guid)) value = Guid.Parse(stringValue);
        else if (Descriptor.ParameterType == typeof(Guid?)) value = string.IsNullOrWhiteSpace(stringValue) ? (Guid?)null : Guid.Parse(stringValue);
        else if (Descriptor.ParameterType == typeof(bool))
        {
            value = false;
            if (stringValue.Equals("true", StringComparison.OrdinalIgnoreCase) || stringValue.Equals("on", StringComparison.OrdinalIgnoreCase) || stringValue == "1") value = true;
        }
        else if (Descriptor.ParameterType == typeof(bool?))
        {
            value = false;
            if (string.IsNullOrWhiteSpace(stringValue)) value = (bool?)null;
            else
                if (stringValue.Equals("true", StringComparison.OrdinalIgnoreCase) || stringValue.Equals("on", StringComparison.OrdinalIgnoreCase) || stringValue == "1") value = true;
        }
        else if (Descriptor.ParameterType.IsEnum || IsNullableEnum(Descriptor.ParameterType))
        {

            return string.IsNullOrWhiteSpace(stringValue)
                ? null
                : GenUtil.stringToEnum(Descriptor.ParameterType, stringValue);
        }
        else if (Descriptor.ParameterType != typeof(byte[]) && Descriptor.ParameterType.IsArray)
        {
            if (!stringValue.IsNull() && !stringValue.StartsWith("["))
            {
                stringValue = "[" + stringValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)+ "]";
            }
            value = JsonConvert.DeserializeObject(stringValue, Descriptor.ParameterType);
        }
        else value = stringValue;

        return value;
    }

    /// <summary>
    /// Read and cache the request body
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private NameValueCollection TryReadBody(HttpRequestMessage request)
    {
        object result;

        // try to read out of cache first
        if (!request.Properties.TryGetValue(MultipleBodyParameters, out result))
        {
            var contentType = request.Content.Headers.ContentType;

            if (contentType != null)

                switch (contentType.MediaType)
                {
                    case "application/json":
                        {
                            if (AllowJsonContentType)
                            {
                                result = request.Content.ReadAsStringAsync().Result;

                                try
                                {
                                    var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());
                                    request.Properties.Add(MultipleBodyParametersRaw, result);

                                    result = values.Aggregate(new NameValueCollection(),
                                                              (seed, current) =>
                                                              {
                                                                  seed.Add(current.Key, current.Value == null ? "" : current.Value.ToString());
                                                                  return seed;
                                                              });



                                    request.Properties.Add(MultipleBodyParameters, result);
                                }
                                catch (Exception ex)
                                {
                                }

                            }

                        }
                        break;
                    case "application/x-www-form-urlencoded":
                        result = request.Content.ReadAsFormDataAsync().Result;
                        request.Properties.Add(MultipleBodyParameters, result);
                        break;

                    case "multipart/form-data":
                        result = HttpContext.Current.Request.Form;
                        request.Properties.Add(MultipleBodyParameters, result);
                        break;
                }
        }


        return result as NameValueCollection;
    }

    private struct AsyncVoid
    {
    }
}



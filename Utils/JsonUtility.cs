using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace VarProject.FrameWork.Core.Utils
{
    public static class JsonUtility
    {

        public static JsonSerializerSettings ApiJsonSettings;

        private static readonly JsonSerializerSettings DataJsonSettings;

        static JsonUtility()
        {

            //初始化数据Json序列化的设置
            JsonSerializerSettings jsonDataSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver
                {
                    IgnoreSerializableAttribute = true
                },
                NullValueHandling = NullValueHandling.Ignore
            };

            jsonDataSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            DataJsonSettings = jsonDataSettings;



            //初始化Api 返回值的Json序列化的设置
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver
                {
                    IgnoreSerializableAttribute = true
                },
                NullValueHandling = NullValueHandling.Ignore
            };

            settings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ApiJsonSettings = settings;
        }


        /// <summary>
        /// 序列化对象为json格式，主要针对Api数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SerializeApiData(object o)
        {
            return JsonConvert.SerializeObject(o, ApiJsonSettings);
        }

        /// <summary>
        /// 反序列化json字符串到对象，主要针对Api数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeApiData<T>(string json) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(json, ApiJsonSettings);
        }


        /// <summary>
        /// 序列化对象为json格式，主要针对普通存储的数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SerializeData(object o)
        {
            return JsonConvert.SerializeObject(o, DataJsonSettings);
        }

        /// <summary>
        /// 反序列化json字符串到对象，主要针对普通存储的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeData<T>(string json) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(json, DataJsonSettings);
        }

        class CamelCaseExceptDictionaryKeysResolver : CamelCasePropertyNamesContractResolver
        {
            protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
            {
                JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);

                contract.DictionaryKeyResolver = propertyName => propertyName;

                return contract;
            }
        }
    }
}

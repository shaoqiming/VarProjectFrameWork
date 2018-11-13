using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using VarProject.FrameWork.Core.Api;
using VarProject.FrameWork.Core.Enums;


namespace exin.FrameWork.Core.Common
{
    public static class WebApiExtensions
    {


        public static HttpResponseMessage ApiResult(this ApiController controller, ApiReturnCode code)
        {
            return ApiResult(controller, code, "");
        }


        public static HttpResponseMessage ApiResult(this ApiController controller, int code, string message)
        {
            var result = new ApiResult(code, message);
            return ApiResult(controller, result);
        }

        public static HttpResponseMessage ApiResult(this ApiController controller, ApiReturnCode code, string message)
        {
            var result = new ApiResult(code, message);
            return ApiResult(controller, result);
        }



        public static HttpResponseMessage ApiResult<T>(this ApiController controller, T returnObject)
        {
            var result = new ApiResult<T>(returnObject);
            return ApiResultT(controller, result);
        }


        public static HttpResponseMessage ApiResult<T>(this ApiController controller, ApiReturnCode code, T returnObject)
        {
            var result = new ApiResult<T>(code, returnObject);
            return ApiResultT(controller, result);
        }


        public static HttpResponseMessage ApiResult(this ApiController controller, ApiResult result)
        {
            if (controller.Request != null)
            {
                return controller.Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(result));//接收:response.Content.ReadAsStringAsync().Result;
                //StringContent content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "text/json");
                HttpResponseMessage response = new HttpResponseMessage() { Content = content };
                return response;
            }
        }


        public static HttpResponseMessage ApiResultT<T>(this ApiController controller, ApiResult<T> result)
        {
            if (controller.Request != null)
            {
                return controller.Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(result));//接收:response.Content.ReadAsStringAsync().Result;
                //StringContent content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "text/json");
                HttpResponseMessage response = new HttpResponseMessage() { Content = content };
                return response;
            }
        }
    


   

    }
}

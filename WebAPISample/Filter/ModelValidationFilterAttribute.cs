using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace WebAPISample.Filter
{
    public class ModelValidationFilterAttribute:ActionFilterAttribute //模型验证过滤器
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(!actionContext.ModelState.IsValid)
            {
                //模型验证失败，在响应中返回验证错误
                var errors = new Dictionary<string, IEnumerable<string>>();
                foreach (KeyValuePair<string, ModelState> keyValue in actionContext.ModelState)
                {
                    errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage);
                }

                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}

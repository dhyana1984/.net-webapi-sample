using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using WebAPISample.Formatter;
using WebAPISample.Models;
using System.Web.Http.OData.Extensions;

namespace WebAPISample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }

            );

    
            config.Routes.MapHttpRoute(
             name: "PrductsByCategory",
             routeTemplate: "api/{controller}/Category/{category}"
          
            );

            config.Routes.MapHttpRoute(
                name: "DeletePrducts",
                routeTemplate: "api/{controller}/Delete/{id}"

                );

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("ProductsSet");
            //第一个参数是一个路由的名称。你的服务的客户端看不到这个名称的
            //第二个参数是终结点URL的前缀
            //针对产品实体集的URL是http://hostname/odata/ProductSet

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
   
    }
}

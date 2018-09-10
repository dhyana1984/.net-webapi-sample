using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPISample.Formatter;

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
        }
   
    }
}

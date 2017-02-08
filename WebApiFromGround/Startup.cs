using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.IO;
using System.Threading;
using Microsoft.Practices.Unity;

namespace WebApiFromGround
{
    public class Startup
    {
        static int counter = 0;
        public static void Configuration(IAppBuilder app)
        {
            //GlobalConfiguration.Configure(ConfigureWebApi);

            var config = new HttpConfiguration();
            ConfigureWebApi(config);
                        
            Action<IOwinContext> action = ctx =>
            {
                var container = MyUnityContainer.Instance;

                container.RegisterType<MyService>(new PerRequestLifetimeManager(ctx));
                container.RegisterType<MyService2>(new PerRequestLifetimeManager(ctx));
            };

            app.Use(typeof(OwinPerRequestLifetimeManagerMiddleware), action);

            app.UseWebApi(config);

        }

        static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            /*
            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );*/

            config.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "api/v{version}/{controller}.{ext}/{id}",
             defaults: new
             {
                 id = RouteParameter.Optional
             });

            config.Routes.MapHttpRoute(
              name: "SimpleRelationShip",
              routeTemplate: "api/v{version}/{controller}.{ext}/{id}/{actionname}/{actionid}/{subaction}/{subactionid}",
              defaults: new
              {
                  id = RouteParameter.Optional,
                  actionname = RouteParameter.Optional,
                  actionid = RouteParameter.Optional,
                  subaction = RouteParameter.Optional,
                  subactionid = RouteParameter.Optional
              });

            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "text/xml");
        }

        static string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }
    }
}

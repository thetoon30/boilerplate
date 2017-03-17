using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjectorExample.Models;
using SimpleInjectorExample.Repository;

namespace SimpleInjectorExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            #region SimpleInjector
            var container = new Container();
            container.Register<IRepository<Actor>, ActorRepository>(Lifestyle.Singleton);
            container.Register<IRepository<Singer>, SingerRepository>(Lifestyle.Singleton);
            container.RegisterWebApiControllers(config);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            #endregion

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

using angular2inter.Core;
using angular2inter.Core.DataContext;
using angular2inter.Core.Infrastructure.Repository;
using angular2inter.Core.Helper;
using angular2inter.Core.Model;
using angular2inter.EF;
using angular2inter.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;

namespace angular2inter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            #region SimpleInjector
            var container = new Container();
            // Set the scoped lifestyle one directly after creating the container
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            // Use the Lifestyle.Scoped everywhere in your configuration.
            container.Register<IDbContext, ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<IPersonRepository, PersonRepository>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IFileSystemUtility, FileSystemUtility>(Lifestyle.Scoped);

            container.RegisterWebApiRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            container.RegisterWebApiRequest<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(new ApplicationDbContext()));
            container.RegisterWebApiRequest<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            container.RegisterWebApiRequest<ITextEncoder, Base64UrlTextEncoder>();
            container.RegisterWebApiRequest<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
            container.RegisterWebApiRequest<IDataProtector>(() => new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider().Create("ASP.NET Identity"));

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

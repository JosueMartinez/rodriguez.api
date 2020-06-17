using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Rodriguez.Data.Models;
using Rodriguez.Repo;
using Rodriguez.Repo.Interfaces;
using Rodriguez.Services;
using Rodriguez.Services.Interfaces;
using System;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(rodriguez.api.Clases.Startup))]
namespace rodriguez.api.Clases
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            ConfigureOAuth(app);
            var config = new HttpConfiguration();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            #region dependencies

            builder.RegisterType<RodriguezModel>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<BonoService>().As<IBonoService>();

            #endregion

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new Providers.SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
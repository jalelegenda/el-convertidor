using System.Web.Mvc;
using System.Web.Routing;
using ElConvertidor.Web;
using Autofac;
using Autofac.Integration.Mvc;
using ElConvertidor.Business;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Web.Services;
using ElConvertidor.Core.Client;
using ElConvertidor.Data.Services;
using ElConvertidor.Core.Data;
using ElConvertidor.Data.Contexts;
using System.Data.Entity;

namespace ElConvertidor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<ImageProcessingService>()
                .As<IImageProcessingService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConversionsStore>()
                .As<IConversionsStore>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SessionService<>))
                .As(typeof(ISessionService<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ImageValidationService>()
                .As<IImageValidationService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConversionsContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


    }
}

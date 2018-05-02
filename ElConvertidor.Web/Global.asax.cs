using System.Web.Mvc;
using System.Web.Routing;
using ElConvertidor.Web;
using Autofac;
using Autofac.Integration.Mvc;
using ElConvertidor.Infrastructure;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Web.Services;
using ElConvertidor.Core.Client;

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
            builder.RegisterGeneric(typeof(SessionService<>))
                .As(typeof(ISessionService<>))
                .InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}

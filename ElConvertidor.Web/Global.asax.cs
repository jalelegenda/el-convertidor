using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ElConvertidor.Web;
using Autofac;
using Autofac.Integration.Mvc;
using ElConvertidor.Web.Controllers;
using ElConvertidor.Infrastructure;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Core;

namespace ElConvertidor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<TiffService>()
                .As<ITiffService>()
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

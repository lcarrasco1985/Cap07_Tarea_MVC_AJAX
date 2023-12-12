using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cap07_Tarea_MVC_AJAX.WEB.Models;
using Cap07_Tarea_MVC_AJAX.CORE.Repository;
using SimpleInjector.Integration.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cap07_Tarea_MVC_AJAX.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region Dependencias

            var container = new SimpleInjector.Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            var connectionString = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;

            container.Register<IDbConnection>(() => new SqlConnection(connectionString), Lifestyle.Scoped);
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>
                (container.GetInstance<ApplicationDbContext>()), Lifestyle.Singleton);
            //Registro del repositorio
            container.Register<IProductRepository>
                (() => new ProductRepository(container.GetInstance<IDbConnection>()));

            //container.Register<IMessageRepository>(() => new MessageRepository(container.GetInstance<IDbConnection>()));

            container.Register<IAuthenticationManager>
                (() => HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Singleton);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Register<ApplicationUserManager>(Lifestyle.Singleton);
            container.Register<ApplicationSignInManager>(Lifestyle.Singleton);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            #endregion
        }
    }
}

using BAISTGOLF.COM.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Castle.Windsor.Installer;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Helpers;
using Castle.Windsor;
using BAISTGOLF.COM.WindsorConfiguration;

namespace BAISTGOLF.COM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private WindsorContainer _windsorContainer;
        protected void Application_Start()
        {
            InitializeWindsor();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeDbMigrations();
            ConfigureAutoMapper();
           

        }
        private void InitializeDbMigrations()
        {
          Database.SetInitializer(new MigrateDatabaseToLatestVersion<BAISTGolfCourseDbContext, TheBackEndLayer.Migrations.Configuration>());
        }

        private void ConfigureAutoMapper()
        {
            new AutoMapperConfigurator().Configure(_windsorContainer.ResolveAll<IAutoMapperTypeConfigurator>());
        }
        protected void Application_End()
        {
            if (_windsorContainer != null)
            {
                _windsorContainer.Dispose();
            }
        }
        private void InitializeWindsor()
        {
            _windsorContainer = new WindsorContainer();
            _windsorContainer.Install(FromAssembly.This());

            // clean up, application exits

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_windsorContainer.Kernel));


        }
        //protected void Application_BeginRequest(object sender, EventArgs args)
        //{
        //    System.Diagnostics.Debug.WriteLine(this.Request.RequestType + " " + this.Request.RawUrl);

        //}

    }
}

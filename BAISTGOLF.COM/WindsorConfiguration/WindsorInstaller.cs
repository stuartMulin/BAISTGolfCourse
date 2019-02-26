using Castle.MicroKernel.Registration;
using System.Web.Mvc;
using System.Reflection;
using Castle.Windsor;
using System.Data.Entity;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheBackEndLayer.Services;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Infrastructure;

namespace BAISTGOLF.COM.WindsorConfiguration
{
    public class WindsorInstaller: IWindsorInstaller
    {
         public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            //Register all controllers
            container.Register(
                //All services
                Classes.FromAssembly(Assembly.GetAssembly(typeof(IAppService))).
                    InSameNamespaceAs<AppService>().WithService.DefaultInterfaces().LifestylePerWebRequest(),

                //All MVC controllers
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient(),

                 //All DbContexts
                 Component.For<DbContext>()
                                    .ImplementedBy<BAISTGolfCourseDbContext>().LifestylePerWebRequest(),

               //All AutoMapper Classes
               Component.For<IAutoMapper>().ImplementedBy<AutoMapperAdapter>().LifestyleTransient()
                );

            //Register Facilities
            container.AddFacility<EntityFrameWorkRelatedFacility>();
            container.AddFacility<GeneralFacility>();
        }
    }
    }

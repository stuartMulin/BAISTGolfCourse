using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using TheBackEndLayer.Repositories;


namespace BAISTGOLF.COM.WindsorConfiguration
{
    public class EntityFrameWorkRelatedFacility : AbstractFacility
    {
        protected override void Init()
        {
            //Repositories
            Kernel.Register(Classes.FromAssembly(Assembly.GetAssembly(typeof(IApplicantsRepository))).
                    InSameNamespaceAs<ApplicantsRepository>().WithService.DefaultInterfaces().LifestylePerWebRequest());

        }
    }
}


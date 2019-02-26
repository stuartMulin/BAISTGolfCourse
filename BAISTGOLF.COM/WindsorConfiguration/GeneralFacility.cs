using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using System.Threading.Tasks;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.Infrastructure;
using TheBackEndLayer.Infrastructure.Maps;

namespace BAISTGOLF.COM.WindsorConfiguration
{
    class GeneralFacility : AbstractFacility
    {
        protected override void Init()
        {
            //ViewModels
            Kernel.Register(Component.For<IAutoMapperTypeConfigurator>()
                 .ImplementedBy<CreateInPutModelApplicant>(),
                 Component.For<IAutoMapperTypeConfigurator>()
                 .ImplementedBy<EmpViewModel>(),
                 Component.For<IAutoMapperTypeConfigurator>()
                 .ImplementedBy<MembersViewModel>(),
                  Component.For<IAutoMapperTypeConfigurator>()
                 .ImplementedBy<ReservationsViewModel>());
        }
    }
}

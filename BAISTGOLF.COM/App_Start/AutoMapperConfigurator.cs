using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBackEndLayer.Helpers;

namespace BAISTGOLF.COM.App_Start
{
    public class AutoMapperConfigurator
    {
        public void Configure(IEnumerable<IAutoMapperTypeConfigurator> autoMapperTypeConfigurators)
        {
            autoMapperTypeConfigurators.ToList().ForEach(x => x.Configure());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
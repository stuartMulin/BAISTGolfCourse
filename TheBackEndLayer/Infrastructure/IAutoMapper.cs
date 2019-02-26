using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Helpers
{
    public interface IAutoMapper
    {
        T Map<T>(object objectToMap);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.GolfCourse
{
   public class GolfCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Founded { get; set; }
        public string City { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBackEndLayer.DbModels
{
    public class GolfCourse
    {
        public GolfCourse()
        {
            TeeTimes = new HashSet<TeeTime>();
        }
        public int ID { get; set; }
        public string CourseName { get; set; }
        public double Rating { get; set; }
        public int Slope { get; set; }
        public ICollection<TeeTime> TeeTimes { get; set; }
    }
}
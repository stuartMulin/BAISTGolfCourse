using TheBackEndLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheBackEndLayer.DbModels
{
    public class TeeTime
    {
        public TeeTime()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public TeeTimeStatus Status { get; set; }
        public int GolfCourseID { get; set; }

        [ForeignKey("GolfCourseID")]
        public GolfCourse GolfCourse { get; set; }

        public ICollection<Reservations> Reservations { get; set; }
    }
}
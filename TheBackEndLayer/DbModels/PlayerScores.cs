using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheBackEndLayer.DbModels
{
    public class PlayerScores
    {      
        public long  Id { get; set; }
        public int CourseId { get; set; }
        public int HoleId { get; set; }
        public int HandicapId { get; set; }
        public int ReservationID { get; set; } 
        public int MemberID { get; set; }      
        public int Rating { get; set; }
        public double Score { get; set; }
        public int Slope { get; set; }
       
        public DateTime DateCreated { get; set; }
        public DateTime DatePlayed { get; set; }

        [ForeignKey("ReservationID")]
        public Reservations Reservation { get; set; }
        public Hole Hole { get; set; }

        [ForeignKey("HandicapId")]
        public HandiCap Handicap { get; set; }

        [ForeignKey("MemberID")]
        public Members Member { get; set; }
    }
}
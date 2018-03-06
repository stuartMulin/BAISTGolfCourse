using TheBackEndLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheBackEndLayer.DbModels
{
    public class Reservations
    {
        public Reservations()
        {
            PlayerScores = new HashSet<Scores>();
        }
        public int ID { get; set; }
        public int TeeTimeID { get; set; }
        public int MemberID { get; set; }

        public bool IsApproved { get; set; }

        [ForeignKey("TeeTimeID")]
        public TeeTime TeeTime { get; set; }
        public DateTime DateCreated { get; set; }
        public ReservationStatus Status { get; set; }
        public ReservationType Type { get; set; }

        [ForeignKey("MemberID")]
        public Members Member { get; set; }
        public virtual ICollection<Scores> PlayerScores { get; set; }
    }   
}
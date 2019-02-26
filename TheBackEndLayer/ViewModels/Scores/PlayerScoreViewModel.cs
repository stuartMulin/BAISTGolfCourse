using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.Scores
{
   public  class PlayerScoreViewModel
    {
        public string MemberName { get; set; }
        public string Gender { get; set; }
        public string Score { get; set; }
        public string Handicap { get; set; }
        public DateTime Date { get; set; }
        public long Id { get; set; }
        public int CourseId { get; set; }
        public int HandicapId { get; set; }
        public int MemberId { get; set; }
        public int Rating { get; set; }
        public int ReservationId { get; set; }
        
    }
}

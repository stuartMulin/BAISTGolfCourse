using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Enums;
using TheBackEndLayer.ViewModels.Members;

namespace TheBackEndLayer.ViewModels.Reservations
{
   public class TeeTimeWithMembersViewModel
       
    {
        public TeeTimeWithMembersViewModel()

        {
            MembersOnReservation = new List<MembersViewModel>();

        }
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TeeTimeStatus Status { get; set; }
        public List<MembersViewModel> MembersOnReservation { get; set; }
    }
}

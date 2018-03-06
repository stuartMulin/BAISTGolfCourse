using TheBackEndLayer.Enums;
using TheBackEndLayer.ViewModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.ViewModels.Reservation
{
    public class MemberTeeTimeViewModel
    {
        public MemberTeeTimeViewModel()
        {
            MembersReserved = new List<MembersViewModel>();
        }
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TeeTimeStatus Status { get; set; }
        public List<MembersViewModel> MembersReserved { get; set; }

    }
}

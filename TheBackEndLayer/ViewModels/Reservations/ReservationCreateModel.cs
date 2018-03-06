using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Enums;

namespace TheBackEndLayer.ViewModels.Reservations
{
    public class ReservationCreateModel
    {
        public int MemberID { get; set; }
        public ReservationStatus Status { get; set; }
    }
}

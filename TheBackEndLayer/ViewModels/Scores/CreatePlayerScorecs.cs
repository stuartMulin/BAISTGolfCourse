using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.Reservations;

namespace TheBackEndLayer.ViewModels.Scores
{
    class CreatePlayerModel
    {
        public int TeeTimeID { get; set; }
        public List<ReservationCreateModel> PotentialReservations { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.Reservations;

namespace TheBackEndLayer.Services
{
    public interface ITeeTimesService
    {
        TeeTimeWithMembersViewModel GetWithMembers(int id);
    }
}

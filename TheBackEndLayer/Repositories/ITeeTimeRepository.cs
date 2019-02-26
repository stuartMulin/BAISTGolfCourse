using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
    public interface ITeeTimeRepository: IgRepository<TeeTime>
    {
        //TeeTime GetWithReservationsById(int id);
        List<TeeTime> GetList(DateTime startDate, DateTime endDate);
        TeeTime GetWithMembers(int id);
    }
}

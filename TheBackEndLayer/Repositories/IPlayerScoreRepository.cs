using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.InViewModels;
using TheBackEndLayer.Services;

namespace TheBackEndLayer.Repositories
{
    public interface IPlayerScoreRepository : IgRepository<PlayerScores>
    {
        List<PlayerScores> GetAllPlayerScores();
        List<PlayerScores> GetPlayerScores(int memberId);
    }
}


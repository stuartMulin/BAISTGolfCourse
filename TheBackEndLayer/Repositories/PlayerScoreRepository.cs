using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using TheBackEndLayer.DbModels;
using EntityState = System.Data.Entity.EntityState;
using TheBackEndLayer.InViewModels;

namespace TheBackEndLayer.Repositories
{
    public class PlayerScoreRepository: GenericRepository<PlayerScores>, IPlayerScoreRepository
    {
        public PlayerScoreRepository(DbContext context) : base(context)
        {
        }
       
        public List<PlayerScores> GetAllPlayerScores()
        {
            return DbSet
                .Include(x => x.Member)
                .Include(x => x.Handicap)
                .OrderBy(x => x.Member.FirstName).ToList();
        }

        public List<PlayerScores> GetPlayerScores(int memberId)
        {
            return DbSet.Include(x => x.Member)
                .Where(x => x.MemberID == memberId).ToList();
        }
    }
}

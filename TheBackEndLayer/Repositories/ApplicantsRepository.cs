using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
  public  class ApplicantsRepository:GenericRepository<Applicants>,IApplicantsRepository
    {
        public ApplicantsRepository(DbContext context) : base(context)
        {
        }
        public List<Applicants> GetAllNewApplicants()
        {
            return DbSet.Include(x => x.ShareHolder1)
                        .Include(x => x.ShareHolder2)
                        .Where(x => x.Status == Enums.ApplicantStatus.Initiated
                        || x.Status == Enums.ApplicantStatus.UnderReview).ToList();
        }
        public Applicants GetWithMembers(int id)
        {
            return DbSet.Include(x => x.ShareHolder1)
                        .Include(x => x.ShareHolder2)
                        .SingleOrDefault(x => x.ID == id);
        }
    }
}

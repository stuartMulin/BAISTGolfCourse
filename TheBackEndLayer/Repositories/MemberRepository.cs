using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
   public class MemberRepository :GenericRepository<Members>,IMemberRepository
    {
        public MemberRepository(DbContext context) : base(context)
        {

        }
       
        
            }
        }
    

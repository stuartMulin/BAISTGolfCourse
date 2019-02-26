﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;

namespace TheBackEndLayer.Repositories
{
   public interface IApplicantsRepository:IgRepository<Applicants>
    {
        List<Applicants> GetAllNewApplicants();
        Applicants GetWithMembers(int id);
    }
}


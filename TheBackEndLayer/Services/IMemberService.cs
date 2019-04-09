using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.Members;

namespace TheBackEndLayer.Services
{
    public interface IMemberService
    {
        MembersViewModel GetMemberByEmail(string emailAddress);
        MembersViewModel GetMemberByMembershipID(string membershipID);
        MembersViewModel GetMemberById(int id);
        MembersViewModel PopulateViewModel(Members member);
        Members GetMemberByMembershipNumber(string memberID);
    }
}

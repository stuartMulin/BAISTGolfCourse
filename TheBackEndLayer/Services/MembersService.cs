using TheBackEndLayer.DbModels;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.ViewModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Services
{
   public class MembersService

    {
        public MembersViewModel GetuserByEmail(string EmailAddress)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var member = db.Members.SingleOrDefault(x => x.EmailAddress == EmailAddress);

                if (member != null)
                {
                    return PopulateViewModel(member);
                }
                else
                {
                    return null;
                }
            }
        }

        public MembersViewModel GetuserByEmailSearch(string EmailAddress)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var member = db.Members.SingleOrDefault(x => x.EmailAddress == EmailAddress);

                if (member != null)
                {
                    return PopulateViewModel(member);
                }
                else
                {
                    return new MembersViewModel();
                }
            }
        }

        private MembersViewModel PopulateViewModel(Members member)
        {
            var memberViewModel = new MembersViewModel
            {
                
                ID = member.ID,
                Address1 = member.Address1,
                EmailAddress = member.EmailAddress,
                DateOfBirth = member.DateOfBirth,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Gender = member.Gender.ToString(),
                Password = member.Password,
                PasswordSalt = member.PasswordSalt
            };

            return memberViewModel;
        }

        public MembersViewModel GetuserById(int Id)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var member = db.Members.SingleOrDefault(x => x.ID == Id);

                if (member != null)
                {
                    return PopulateViewModel(member);
                }
                else
                {
                    return null;
                }
            }
        }

        public MembersViewModel ValidateUser(string passwordEntered, MembersViewModel member)
        {
            var hashedPassword = PasswordEncryptor.CreatePasswordHash(passwordEntered,
                 member.PasswordSalt);

            if (hashedPassword.Equals(member.Password))
            {
                return member;
            }
            else
                return null;
        }
    }

}

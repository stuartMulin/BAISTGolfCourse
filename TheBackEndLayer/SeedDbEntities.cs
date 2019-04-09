using TheBackEndLayer.DbModels;
using TheBackEndLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer
{
    public static class SeedDbEntities
    {
        public static void SeedDb(BAISTGolfCourseDbContext database)
        {
            var employee1 = new Employees
            {
                FirstName = "Clerk",
                LastName = "Mason",
                EmailAddress = "ClarkMason@gmail.com",
                Address1 = "630 McAllister Loop SW",
                Phone = "(647)608-1541",
                DateCreated = DateTime.UtcNow,
                DateOfBirth = new DateTime(1993, 2, 19),
                Gender = Enums.Gender.Male,
                Role = Enums.Role.Administrator,
                UserName = "admin1",
                PCode = "T6E 123",
                Province = "Alberta",
                City = "Edmonton"
            };

            employee1.PasswordSalt = PasswordEncryptor.CreateSalt(5);
            employee1.Password = PasswordEncryptor.CreatePasswordHash("Course2019@",
                employee1.PasswordSalt);

            var member1 = new Members
            {
                FirstName = "Stuart",
                LastName = "Mason",
                EmailAddress = "stuartMember@gmail.com",
                Address1 = "630 McAllister Loop SW",
                Phone = "(647)608-1541",
                DateCreated = DateTime.UtcNow,
                DateOfBirth = new DateTime(1989, 2, 9),
                Gender = Enums.Gender.Female,
                City = "Toronto",
                Province = "Ontario",
                PostalCode = "T6W 1N3",
                MembershipID = "123456A",
                MembershipType = Enums.MembershipStatus.Gold
            };


            var counter = 6;

            for (int i = 0; i < counter; i++)
            {
                var memberCreate = new Members
                {
                    FirstName = "Stuart" + i,
                    LastName = "Mason" + i,
                    EmailAddress = "stuartMember" + i +"@gmail.com",
                    Address1 = "630 McAllister Loop SW",
                    Phone = "(647)608-1541",
                    DateCreated = DateTime.UtcNow,
                    DateOfBirth = new DateTime(1989, 2, (9 + i)),
                    Gender = Enums.Gender.Female,
                    City = "Toronto",
                    Province = "Ontario",
                    PostalCode = "T6W 1N3",
                    MembershipType = Enums.MembershipStatus.Gold,
                    MembershipID = "12356A" + i,
                };

                memberCreate.PasswordSalt = PasswordEncryptor.CreateSalt(5);
                memberCreate.Password = PasswordEncryptor.CreatePasswordHash("Course2019",
                    memberCreate.PasswordSalt);

                database.Members.AddOrUpdate(p => p.EmailAddress, memberCreate);
            }

            member1.PasswordSalt = PasswordEncryptor.CreateSalt(5);
            member1.Password = PasswordEncryptor.CreatePasswordHash("Course2019",
                member1.PasswordSalt);

            database.Members.AddOrUpdate(p => p.EmailAddress, member1);
            database.Employees.AddOrUpdate(p => p.EmailAddress, employee1);

            var golfClub = new GolfCourse
            {
                CourseName = "Green Hill",
                Rating = 70,
                Slope = 5,
                DateAdded = DateTime.UtcNow,
                YearFounded = new DateTime(1998, 2, 19)
            };

            var golfClub2 = new GolfCourse
            {
                CourseName = "Flat Green",
                Rating = 75,
                Slope = 5,
                DateAdded = DateTime.UtcNow,
                YearFounded = new DateTime(1998, 2, 19)
            };

            database.GolfCourses.AddOrUpdate(p => p.ID, golfClub);
            database.GolfCourses.AddOrUpdate(p => p.ID, golfClub2);

            database.SaveChanges();
            DateTime now = DateTime.Now;
            var validStartTeeTime = new DateTime(now.Year, /*DateTime.*/now.Month,
               /* DateTime*/now.Day + 1, 10, 0, 0);

            var teeTime = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = validStartTeeTime,
                EndDate = validStartTeeTime.AddMinutes(7),
                Status = Enums.TeeTimeStatus.Open,
                GolfCourseID = golfClub.ID
            };
            var teeTimeTwo = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = validStartTeeTime.AddMinutes(14),
                EndDate = validStartTeeTime.AddMinutes(21),
                Status = Enums.TeeTimeStatus.Open,
                GolfCourseID = golfClub2.ID
            };

            var teeTime3 = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = validStartTeeTime.AddMinutes(28),
                EndDate = validStartTeeTime.AddMinutes(35),
                Status = Enums.TeeTimeStatus.Open,
                GolfCourseID = golfClub.ID
            };


            var teeTime4 = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = validStartTeeTime.AddMinutes(42),
                EndDate = validStartTeeTime.AddMinutes(49),
                Status = Enums.TeeTimeStatus.Open,
                GolfCourseID = golfClub2.ID
            };

            var teeTime5 = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = validStartTeeTime.AddMinutes(56),
                EndDate = validStartTeeTime.AddMinutes(63),
                Status = Enums.TeeTimeStatus.Open,
                GolfCourseID = golfClub.ID
            };

            database.TeeTime.AddOrUpdate(p => new { p.GolfCourseID, p.StartDate }, teeTime);
            database.TeeTime.AddOrUpdate(p => new { p.GolfCourseID, p.StartDate }, teeTimeTwo);
            database.TeeTime.AddOrUpdate(p => new { p.GolfCourseID, p.StartDate }, teeTime3);
            database.TeeTime.AddOrUpdate(p => new { p.GolfCourseID, p.StartDate }, teeTime4);
            database.TeeTime.AddOrUpdate(p => new { p.GolfCourseID, p.StartDate }, teeTime5);


            database.SaveChanges();
        }
    }
}

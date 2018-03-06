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
                EmailAddress = "stuart@gmail.com",
                Address = "630 McAllister Loop SW",
                Phone = "(647)608-1541",
                DateCreated = DateTime.UtcNow,
                DateOfBirth = new DateTime(1993, 2, 19),
                Gender = Enums.Gender.Male,
                Role = Enums.Role.Administrator,
                UserName = "admin1",
            };

            employee1.PasswordSalt = PasswordEncryptor.CreateSalt(5);
            employee1.Password = PasswordEncryptor.CreatePasswordHash("football2",
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
                PostalCode = "T6W 1N3"
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
                    DateOfBirth = new DateTime(1989, 2, 9 + i),
                    Gender = Enums.Gender.Female,
                    City = "Toronto",
                    Province = "Ontario",
                    PostalCode = "T6W 1N3"
                };

                memberCreate.PasswordSalt = PasswordEncryptor.CreateSalt(5);
                memberCreate.Password = PasswordEncryptor.CreatePasswordHash("football2",
                    memberCreate.PasswordSalt);

                database.Members.AddOrUpdate(p => p.EmailAddress, memberCreate);
            }

            member1.PasswordSalt = PasswordEncryptor.CreateSalt(5);
            member1.Password = PasswordEncryptor.CreatePasswordHash("football2",
                member1.PasswordSalt);

            database.Members.AddOrUpdate(p => p.EmailAddress, member1);
            database.Employees.AddOrUpdate(p => p.EmailAddress, employee1);

            var golfClub = new GolfCourse
            {
                CourseName = "BAIST Golf Course",
                Rating = 70,
                Slope = 5

            };

            var golfClub2 = new GolfCourse
            {
                CourseName = "Accounting Golf Course",
                Rating = 75,
                Slope = 5
            };

            database.GolfCourses.AddOrUpdate(p => p.ID, golfClub);
            database.GolfCourses.AddOrUpdate(p => p.ID, golfClub2);

            database.SaveChanges();

            var teeTime = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddMinutes(15),
                EndDate = DateTime.Now.AddMinutes(45),
                TeeState = Enums.TeeTimeStatus.Open,
                GolfCourseID = 1
            };
            var teeTimeTwo = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddMinutes(60),
                EndDate = DateTime.Now.AddMinutes(90),
                TeeState = Enums.TeeTimeStatus.Open,
                GolfCourseID = 1
            };

            var teeTime3 = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddMinutes(120),
                EndDate = DateTime.Now.AddMinutes(150),
                TeeState = Enums.TeeTimeStatus.Open,
                GolfCourseID = 1
            };


            var teeTime4 = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddMinutes(180),
                EndDate = DateTime.Now.AddMinutes(210),
                TeeState = Enums.TeeTimeStatus.Open,
                GolfCourseID = 1
            };

            var teeTime5 = new TeeTime
            {
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddMinutes(240),
                EndDate = DateTime.Now.AddMinutes(270),
                TeeState = Enums.TeeTimeStatus.Open,
                GolfCourseID = 1
            };

            database.TeeTime.AddOrUpdate(p => p.Id, teeTime);
            database.TeeTime.AddOrUpdate(p => p.Id, teeTimeTwo);
            database.TeeTime.AddOrUpdate(p => p.Id, teeTime3);
            database.TeeTime.AddOrUpdate(p => p.Id, teeTime4);
            database.TeeTime.AddOrUpdate(p => p.Id, teeTime5);


            database.SaveChanges();
        }
    }
}

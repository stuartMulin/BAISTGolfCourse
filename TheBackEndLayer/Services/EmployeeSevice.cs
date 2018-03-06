using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.Employee;
using TheBackEndLayer.ViewModels;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Helpers;

namespace TheBackEndLayer.Services
{
   public class EmployeeService
    {
        public EmployeeViewModel GetuserByEmail(string EmailAddress)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var employee = db.Employees.SingleOrDefault(x => x.EmailAddress == EmailAddress);

                if (employee != null)
                {
                    return PopulateEmployeeViewModel(employee);
                }
                else
                {
                    return null;
                }
            }
        }

        private EmployeeViewModel PopulateEmployeeViewModel(Employees employee)
        {
            var employeeViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Address = employee.Address,
                EmailAddress = employee.EmailAddress,
                DateOfBirth = employee.DateOfBirth,
                UserName = employee.UserName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender.ToString(),
                Password = employee.Password,
                PasswordSalt = employee.PasswordSalt
            };

            return employeeViewModel;
        }

        public EmployeeViewModel GetuserById(int Id)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var employee = db.Employees.SingleOrDefault(x => x.Id == Id);

                if (employee != null)
                {
                    return PopulateEmployeeViewModel(employee);
                }
                else
                {
                    return null;
                }
            }
        }

        public EmployeeViewModel ValidateUser(string passwordEntered, EmployeeViewModel employee)
        {
            var hashedPassword = PasswordEncryptor.CreatePasswordHash(passwordEntered,
                 employee.PasswordSalt);

            if (hashedPassword.Equals(employee.Password))
            {
                return employee;
            }
            else
                return null;
        }

        public bool ReservationAccepted(string action, int id)
        {
            return false;
        }
    }
}

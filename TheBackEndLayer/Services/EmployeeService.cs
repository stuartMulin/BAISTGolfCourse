using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.Employee;
using TheBackEndLayer.ViewModels;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.Repositories;
using TheBackEndLayer.ViewModels.Applicants;

namespace TheBackEndLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IApplicantsRepository _applicantsRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IEmpRepository _employeeRepository;
        private readonly IAutoMapper _autoMapper;
        public EmployeeService(IApplicantsRepository applicantsRepository,
            IMemberRepository memberRepository,
            IEmpRepository employeeRepository, IAutoMapper autoMapper)
        {

            _memberRepository = memberRepository;
            _employeeRepository = employeeRepository;
            _autoMapper = autoMapper;
            _applicantsRepository = applicantsRepository;
        }
        public EmployeeViewModel GetUserByEmail(string emailAddress)
        {
            var user = _employeeRepository.FindBy(x => x.EmailAddress == emailAddress)
                .SingleOrDefault();
            if (user == null)
                return null;

            var employeeViewModel = _autoMapper.Map<EmployeeViewModel>(user);
            return employeeViewModel;
        }

        public EmployeeViewModel GetUserByID(int id)
        {
            var user = _employeeRepository.GetSingle(id);
            if (user == null)
                return null;

            var employeeViewModel = _autoMapper.Map<EmployeeViewModel>(user);
            return employeeViewModel;
        }
        public List<AppRequestViewModel> GetAllNewApplicants()
        {
            var newApplicants = _applicantsRepository.GetAllNewApplicants();

            var newApplicantsViewModel = _autoMapper.Map<List<AppRequestViewModel>>(newApplicants);

            return newApplicantsViewModel;
        }
        public EmployeeViewModel ValidateUser(string passwordProvided, EmployeeViewModel employee)
        {
            var hashedPassword = PasswordEncryptor.CreatePasswordHash(passwordProvided,
                employee.PasswordSalt);
            if (hashedPassword.Equals(employee.Password))
            {
                return employee;
            }
            else
                return null;
        }
        public bool MakeDecision(string action, int id)
        {
            var applicant = _applicantsRepository.GetSingle(id);
            if (action == "approve")
            {
                applicant.Status = Enums.ApplicantStatus.Approved;
                _applicantsRepository.SaveChanges();

                //Create Member
                var member = _autoMapper.Map<Members>(applicant);
                member.ID = applicant.ID;
                member.DateCreated = DateTime.UtcNow;
                member.MembershipID = Helpers.Encorder.Conceal(applicant.ID, 8);


                _memberRepository.Add(member);
                _memberRepository.SaveChanges();

                return true;
            }
            else
            {
                applicant.Status = Enums.ApplicantStatus.Rejected;
                _applicantsRepository.SaveChanges();

                return false;

            }
        }
    }
}

            //        private EmployeeViewModel PopulateEmployeeViewModel(Employees employee)
            //        {
            //            var employeeViewModel = new EmployeeViewModel
            //            {
            //                Id = employee.Id,
            //                Address = employee.Address,
            //                EmailAddress = employee.EmailAddress,
            //                DateOfBirth = employee.DateOfBirth,
            //                UserName = employee.UserName,
            //                FirstName = employee.FirstName,
            //                LastName = employee.LastName,
            //                Gender = employee.Gender.ToString(),
            //                Password = employee.Password,
            //                PasswordSalt = employee.PasswordSalt
            //            };

//            return employeeViewModel;
//        }

//        public EmployeeViewModel GetuserById(int Id)
//        {
//            var user = _employeeRepository.GetSingle(Id);
//            if (user == null)
//                return null;

//            var employeeViewModel = _autoMapper.Map<EmployeeViewModel>(user);
//            return employeeViewModel;

//        }
//        public List<ApplicantsViewModel> GetAllNewApplicants()
//        {
//            var newApplicants = _applicantsRepository.GetAllNewApplicants();

//            var newApplicantsViewModel = _autoMapper.Map<List<ApplicantsViewModel>>(newApplicants);

//            return newApplicantsViewModel;
//        }
//        public EmployeeViewModel ValidateUser(string passwordEntered, EmployeeViewModel employee)
//        {
//            var hashedPassword = PasswordEncryptor.CreatePasswordHash(passwordEntered,
//                 employee.PasswordSalt);

//            if (hashedPassword.Equals(employee.Password))
//            {
//                return employee;
//            }
//            else
//                return null;
//        }

//        public bool ReservationAccepted(string action, int id)
//        {
//            return false;
//        }



//        EmployeeViewModel IEmpService.GetUserByEmail(string EmailAddress)
//        {
//            throw new NotImplementedException();
//        }

//        EmployeeViewModel IEmpService.GetUserByID(int id)
//        {
//            throw new NotImplementedException();
//        }

//        EmployeeViewModel IEmpService.ValidateUser(string passwordProvided, EmployeeViewModel applicant)
//        {
//            throw new NotImplementedException();
//        }

//        bool IEmpService.MakeDecision(string action, int id)
//        {
//            throw new NotImplementedException();
//        }

//        List<AppRequestViewModel> IEmpService.GetAllNewApplicants()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

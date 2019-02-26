using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.Applicants;
using TheBackEndLayer.ViewModels.Employee;

namespace TheBackEndLayer.Services
{
    public interface IEmployeeService
    {
        EmployeeViewModel GetUserByEmail(string EmailAddress);
        EmployeeViewModel GetUserByID(int id);
        EmployeeViewModel ValidateUser(string passwordProvided, EmployeeViewModel applicant);
        bool MakeDecision(string action, int id);
        List<AppRequestViewModel> GetAllNewApplicants();
    }
}

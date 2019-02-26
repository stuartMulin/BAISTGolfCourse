using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.InViewModels;
using TheBackEndLayer.ViewModels.Applicants;

namespace TheBackEndLayer.Services
{
   public interface IAppService
    {
        //CreateInputModel Create();
        ApplicantsViewModel CreateApplicant(CreateInputModel inputModel);
        ApplicantsViewModel GetUserByEmail(string EmailAddress);
        ApplicantsViewModel GetUserByID(int id);
        ApplicantsViewModel ValidateUser(string passwordProvided, ApplicantsViewModel applicant);
        AppRequestViewModel GetUserDetailByID(int id);
        AppRequestViewModel GetUserDetailChangeStatusByID(int id);
        void AcceptRejectApplicant(int memberID, int applicantID, int status);
        object Create();
        List<AppReportViewModel> GetAllApplicants();
        List<AppReportViewModel> GetAllApplicantsApproved();
        List<AppReportViewModel> GetAllApplicantsNotApproved();
    }
}

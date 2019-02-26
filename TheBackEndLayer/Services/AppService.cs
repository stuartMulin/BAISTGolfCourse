using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Enums;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.InViewModels;
using TheBackEndLayer.Repositories;
using TheBackEndLayer.ViewModels.Applicants;
using TheBackEndLayer.ViewModels.HandlesInPut;
using CreateInputModel = TheBackEndLayer.ViewModels.HandlesInPut.CreateInputModel;

namespace TheBackEndLayer.Services
{
    public class AppService : IAppService
    {
        private readonly IApplicantsRepository _applicantRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IEmailService _emailService;
        private readonly IAutoMapper _autoMapper;
        public AppService(IApplicantsRepository applicantRepository,
            IMemberRepository memberRepository, IEmailService emailService,IAutoMapper autoMapper)
        {
            _applicantRepository = applicantRepository;
            _memberRepository = memberRepository;
            _emailService = emailService;
            _autoMapper = autoMapper;
            _autoMapper = autoMapper;
        }
        public CreateInputModel Create()
        {
            return new CreateInputModel() { Provinces = ProvincesModel.PopulateProvinces() };
        }

        public ApplicantsViewModel CreateApplicant(CreateInputModel inputModel)
        {
            var applicantModel = _autoMapper.Map<Applicants>(inputModel);
            applicantModel.PasswordSalt = PasswordEncryptor.CreateSalt(5);

            var hashedPassword = PasswordEncryptor.CreatePasswordHash(applicantModel.Password,
                applicantModel.PasswordSalt);

            applicantModel.Password = hashedPassword;

            //Get the 2 shareholders
            var shareHolderOne = _memberRepository.FindBy(x => x.MembershipID == inputModel.
            ShareHolder1MemberID).SingleOrDefault();


            var shareHolderTwo = _memberRepository.FindBy(x => x.MembershipID == inputModel.
            ShareHolder2MemberID).SingleOrDefault();

            applicantModel.ShareHolder1ID = shareHolderOne.ID;
            applicantModel.ShareHolder2ID = shareHolderTwo.ID;

            applicantModel.RejectionReason = RejectionReason.Other;

            _applicantRepository.Add(applicantModel);
            _applicantRepository.SaveChanges();

            //Send Email To Applicant
            _emailService.sendEmailToApplicant(applicantModel.EmailAddress, applicantModel.FirstName);

            var applicantViewModel = _autoMapper.Map<ApplicantsViewModel>(applicantModel);
            return applicantViewModel;
        }
        public ApplicantsViewModel GetUserByEmail(string emailAddress)
        {
            var user = _applicantRepository.FindBy(x => x.EmailAddress == emailAddress)
                .SingleOrDefault();
            if (user == null)
                return null;

            var applicantViewModel = _autoMapper.Map<ApplicantsViewModel>(user);
            return applicantViewModel;
        }
        public ApplicantsViewModel ValidateUser(string passwordProvided, ApplicantsViewModel applicant)
        {
            var hashedPassword = PasswordEncryptor.CreatePasswordHash(passwordProvided,
                applicant.PasswordSalt);
            if (hashedPassword.Equals(applicant.Password))
            {
                return applicant;
            }
            else
                return null;
        }
        public ApplicantsViewModel GetUserByID(int id)
        {
            var user = _applicantRepository.GetSingle(id);
            if (user == null)
                return null;

            var applicantViewModel = _autoMapper.Map<ApplicantsViewModel>(user);
            return applicantViewModel;
        }
        public AppRequestViewModel GetUserDetailByID(int id)
        {
            var user = _applicantRepository.GetWithMembers(id);
            if (user == null)
                return null;

            var applicantViewModel = _autoMapper.Map<AppRequestViewModel>(user);
            return applicantViewModel;
        }
        public void AcceptRejectApplicant(int memberID, int applicantID, int status)
        {
            var applicant = _applicantRepository.GetSingle(applicantID);

            if (status == 0)
            {


                if (applicant.ShareHolder1ID == memberID)
                {
                    applicant.HasShareHolderOneConfirmed = false;
                }
                else if (applicant.ShareHolder2ID == memberID)
                    applicant.HasShareHolderTwoConfirmed = false;

                applicant.Status = ApplicantStatus.Rejected;
                applicant.RejectionReason = RejectionReason.ShareHolderRejected;
                _applicantRepository.SaveChanges();
            }

            if (status == 1)
            {
                if (applicant.ShareHolder1ID == memberID)
                {
                    applicant.HasShareHolderOneConfirmed = true;
                }
                else if (applicant.ShareHolder2ID == memberID)
                    applicant.HasShareHolderTwoConfirmed = true;

                _applicantRepository.SaveChanges();
            }
        }

        public AppRequestViewModel GetUserDetailChangeStatusByID(int id)
        {
            var user = _applicantRepository.GetWithMembers(id);
            if (user == null)
                return null;

            user.Status = ApplicantStatus.UnderReview;
            _applicantRepository.SaveChanges();

            var applicantViewModel =_autoMapper.Map<AppRequestViewModel>(user);

            return applicantViewModel;
        }
        public List<AppReportViewModel> GetAllApplicants()
        {
            var applicants = _applicantRepository.GetAll().
                Select(x => new AppReportViewModel
                {
                    EmailAddress = x.EmailAddress,
                    FullName = x.FirstName + " " + x.LastName,
                    Status = (int)x.Status
                }).ToList();

            return applicants;
        }
        public List<AppReportViewModel> GetAllApplicantsApproved()
        {
            var applicants = _applicantRepository.FindBy(x => x.Status == ApplicantStatus.Approved).
                Select(x => new AppReportViewModel
                {
                    EmailAddress = x.EmailAddress,
                    FullName = x.FirstName + " " + x.LastName,
                    Status = (int)x.Status
                }).ToList();

            return applicants;
        }
        public List<AppReportViewModel> GetAllApplicantsNotApproved()
        {
            var applicants = _applicantRepository.FindBy(x => x.Status != ApplicantStatus.Approved).
                Select(x => new AppReportViewModel
                {
                    EmailAddress = x.EmailAddress,
                    FullName = x.FirstName + " " + x.LastName,
                    Status = (int)x.Status
                }).ToList();

            return applicants;
        }

        public ApplicantsViewModel CreateApplicant(InViewModels.CreateInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        object IAppService.Create()
        {
            throw new NotImplementedException();
        }
    }
}


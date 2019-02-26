using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Services
{
    public interface IEmailService
    {
        //void SendEmailToApplicant(string emailAddress, string firstName);
        void SendEmailToSponsors(string emailAddress, string applicantName, string firstName);
        void SendConfirmationEmailToMember(string emailAddress, string firstName);
        void sendEmailToApplicant(string emailAddress, string FName);
    }

    
}

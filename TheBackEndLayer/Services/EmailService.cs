using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TheBackEndLayer.Services
{
    public class EmailService:IEmailService
    {
        //private readonly string _welcomeEmailPath = "/EmailTemplates/WelcomeEmailTemplate.html";
        private readonly string _sponsorEmailPath = "/EmailTemplates/SponsorEmailTemplate.html";

        public void SendConfirmationEmailToMember(string emailAddress, string firstName)
        {

        }

        public void sendEmailToApplicant(string emailAddress, string FName)
        {
            throw new NotImplementedException();
        }

        public void SendEmailToSponsors(string emailAddress, string applicantName, string firstName)
        {
            var messageHtmlFile = HttpContext.Current.Server.MapPath(_sponsorEmailPath);

            string content = File.ReadAllText(messageHtmlFile);

            content = content.Replace("$userName$", firstName);
            content = content.Replace("$applicantName$", firstName);

            var message = new MailMessage("\"BAISTGolfClub\" <stuart@gmail.com>",
                emailAddress)
            {
                Subject = "An applicant has referenced you!",
                Body = content,
                IsBodyHtml = true

            };

            var client = new SmtpClient();
            client.Send(message);
        }
    }
}

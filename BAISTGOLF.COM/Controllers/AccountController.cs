
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheBackEndLayer.Enums;
using TheBackEndLayer.InViewModels;
using TheBackEndLayer.Services;
using TheBackEndLayer.ViewModels.Login;

namespace BAISTGOLF.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppService _applicantService;
        private readonly IMemberService _memberService;
        private readonly IEmployeeService _employeeSerivice;
        public AccountController(IAppService applicantService,
             IMemberService memberService,IEmployeeService employeeService)
        {
            _applicantService = applicantService;
            _memberService = memberService;
            _employeeSerivice = employeeService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            var applicantInputModel = _applicantService.Create();
            return View(applicantInputModel);
        }

        [HttpPost]
        public ActionResult Register(CreateInputModel createInputModel)
        {
            if (ModelState.IsValid)
            {
                var accountCreated = _applicantService.CreateApplicant(createInputModel);
            }
            return View(createInputModel);
        }

        [HttpPost]
        public JsonResult CheckifEmailExists(string EmailAddress)
        {
            var user = _applicantService.GetUserByEmail(EmailAddress);
            return Json(user == null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckMemberNumberExists(string MemberID)
        {
            var member = _memberService.GetMemberByMembershipNumber(MemberID);
            return Json(member != null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckMembershipNumber1(string ShareHolder1MemberID)
        {
            var member = _memberService.GetMemberByMembershipNumber(ShareHolder1MemberID);
            return Json(member != null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckMembershipNumber2(string ShareHolder2MemberID)
        {
            var member = _memberService.GetMemberByMembershipNumber(ShareHolder2MemberID);
            return Json(member != null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerifySuccess(int memberID, int applicantID)
        {
            _applicantService.AcceptRejectApplicant(memberID, applicantID, 1);
            ViewBag.Status = "Success";
            return View("ThankYouAcceptance", "Home");
        }

        public ActionResult VerifyFail(int memberID, int applicantID)
        {
            _applicantService.AcceptRejectApplicant(memberID, applicantID, 0);
            ViewBag.Status = "Failure";
            return View("ThankYouAcceptance", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginInputModel loginModel)
        {
            
            if (ModelState.IsValid)
            {
                //Store Remember Me in session for later use
                var memberViewModel = _memberService.GetMemberByEmail(loginModel.Email);

                

                FormsAuthentication.SetAuthCookie(memberViewModel.EmailAddress,

                    loginModel.RememberMe);
                return RedirectToAction("MemberAccount", "Members",
                    new { id = memberViewModel.MembershipID });
            }
            else
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(loginModel);
            }
        }
    }
}
using TheBackEndLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAISTGOLF.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppService _applicantService;
        private readonly IMemberService _memberService;

        public HomeController(IAppService applicantService,
           IMemberService memberService)
        {
            _applicantService = applicantService;
            _memberService = memberService;
        }
        public ActionResult Index()
        {
            if (Request.Cookies["IsAdministrator"] != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var id = int.Parse(User.Identity.Name);

                    return RedirectToAction("Dashboard", "Admin",
                                    new { id = id });
                }
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    var memberVieModel = _memberService.GetMemberByEmail(User.Identity.Name);

                    return RedirectToAction("MemberAccount", "Members",
                                new { id = memberVieModel.MembershipID });
                }
                return View();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheBackEndLayer.Services;

namespace BAISTGOLF.Controllers
{
    public class AccountController : Controller
    {
        private EmployeeService employeeService = new EmployeeService();
        private MembersService memberService = new MembersService();
        // GET: Account
        public ActionResult Index()
        {

            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LoginAsMember()
        {
            var email = Request.Form["EmailAddress"];
            var password = Request.Form["Password"];

            var user = memberService.GetuserByEmail(email);

            if (user != null)
            {
                var validatedUser = memberService.ValidateUser(password, user);

                if (validatedUser != null)
                {
                    FormsAuthentication.SetAuthCookie(validatedUser.EmailAddress,
                            true);

                    return RedirectToAction("Detail", "Members", new
                    { id = validatedUser.ID });

                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAsAdmin()
        {
            var email = Request.Form["EmailAddress"];
            var password = Request.Form["Password"];

            var user = employeeService.GetuserByEmail(email);

            if (user != null)
            {
                var validatedUser = employeeService.ValidateUser(password, user);

                if (validatedUser != null)
                {
                    FormsAuthentication.SetAuthCookie(validatedUser.EmailAddress,
                            true);

                    return RedirectToAction("Detail", "Admin", new
                    { id = validatedUser.Id });

                }
            }

            return View();
        }

    }
}

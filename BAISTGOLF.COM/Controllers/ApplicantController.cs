using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBackEndLayer.Services;

namespace BAISTGOLF.COM.Controllers
{
   
        // GET: Applicant
        [Authorize]
        public class ApplicantController : Controller
        {
            private readonly IAppService _applicantService;
            public ApplicantController(IAppService applicantService)
            {
                _applicantService = applicantService;
            }
            public ActionResult Dashboard(int id)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");

                }
                var applicantViewModel = _applicantService.GetUserByID(id);

                return View(applicantViewModel);
            }
        }
    }

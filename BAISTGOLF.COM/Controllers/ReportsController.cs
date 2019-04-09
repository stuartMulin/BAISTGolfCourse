using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBackEndLayer.Services;

namespace BAISTGOLF.COM.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IScoreService _scoreService;
        private readonly IMemberService _memberService;
        private readonly IAppService _appService;


        public ReportsController(IScoreService scoreService, IMemberService memberService, IAppService appService)
        {
            _scoreService = scoreService;
            _memberService = memberService;
            _appService = appService;
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Scores()
        {

            var scores = _scoreService.GetAllUserScores();
            return PartialView(scores);
        }

        public ActionResult scoresByMale()
        {
            var scores = _scoreService.GetAllUserScoresMale();
            return PartialView("Scores", scores);
        }

        public ActionResult Applicants()
        {
            var users = _appService.GetAllApplicants();
            return PartialView("Applicants", users);
        }

        public ActionResult ApplicantsApproved()
        {
            var users = _appService.GetAllApplicantsApproved();
            return PartialView("Applicants", users);
        }
        public ActionResult ApplicantsNotApproved()
        {
            var users = _appService.GetAllApplicantsNotApproved();
            return PartialView("Applicants", users);
        }
    }
}
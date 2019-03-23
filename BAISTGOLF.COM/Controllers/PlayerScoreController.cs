using TheBackEndLayer.Services;
using System.Web.Mvc;
using TheBackEndLayer.ViewModels.Members;
using Microsoft.Ajax.Utilities;
using BAISTGOLF.InViewModels;
using TheBackEndLayer.Repositories;
using System.Data;
using System.Net;
using TheBackEndLayer.InViewModels;

namespace BaistOnlineGolfingSystem.Controllers
{
    [Authorize]
    public class PlayerScoreController : Controller
    {
        private readonly IScoreService _scoreservice;
        public PlayerScoreController(IScoreService scoreService)
        {
            _scoreservice = scoreService;

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnterMemberScore()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EnterPlayerScore()
        {
            var createInputModel = _scoreservice.GetInputModel(User.Identity.Name);
            return PartialView(createInputModel);
        }

        [HttpGet]
        public ActionResult EnterPlayerScoreAdmin()
        {
            var createInputModel = _scoreservice.GetInputModelForAdmin();
            return PartialView(createInputModel);
        }

        [HttpPost]
        public ActionResult EnterPlayerScore(CreateInputModel model)
        {
            model.MemberID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var response = _scoreservice.CreatePlayerScore(model, User.Identity.Name);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public ActionResult EnterPlayerScoreAdmin(CreateInputModelWithMember model)
        {
            if (ModelState.IsValid)
            {
                var response = _scoreservice.CreatePlayerScore(model);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public ActionResult GetPlayerScore()
        {
            var userScore = _scoreservice.GetWeeklyReport(User.Identity.Name);

            return Json(userScore, JsonRequestBehavior.AllowGet);
        }
    }
}
//using System;
//using TheBackEndLayer.Services;
//using System.Collections.Generic;
//using System.Linq;
//using TheBackEndLayer.ViewModels;
//using System.Web;
//using System.Web.Mvc;

//namespace BaistOnlineGolfingSystem.Controllers
//{
//    public class PlayerScoreController : Controller
//    {
//        private ScoreService  scoreService = new ScoreService();
//        private MembersService memberService = new MembersService();
//        // GET: PlayerScore
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult EnterScore()
//        {
//            return View();

//        }

//        [HttpGet]
        
//        public ActionResult EnterScores()
//        {
//            var createInputModel = scoreService.GetScoresbyID(User.Identity.Name);
//            return View(createInputModel);


//        }

//        [HttpGet]
//        public ActionResult EnterScoreByAdmin()
//        {
//            var createInputModel = scoreService.InPutModelByAdmin()
//                return View(createInputModel)

//        }
//    }
//}
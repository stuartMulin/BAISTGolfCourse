using TheBackEndLayer.Services;
using System.Web.Mvc;
using TheBackEndLayer.ViewModels.Reservation;
using TheBackEndLayer.Repositories;
using TheBackEndLayer.ViewModels.HandlesInPutModels;
using System.Data;
using TheBackEndLayer.DbModels;
using System.Net;
using TheBackEndLayer.ViewModels.ForInputModels;
using System.Linq;

namespace BAISTGOLF.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ITeeTimesService _teetimeService;
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService, ITeeTimesService teetimeService, IMemberService memberService)
        {
            _reservationService = reservationService;
            _teetimeService = teetimeService;
            _memberService = memberService;



        }
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            var member = _memberService.GetMemberByEmail(User.Identity.Name);
            ViewBag.MemberID = member.ID;
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateReserveInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var create = _reservationService.CreateReservation(inputModel, User.Identity.Name);
                if (create)
                    return Json("OK", JsonRequestBehavior.AllowGet);
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult FindTeeTime(FindTeeTimeModel teeTimeFinder)
        {
            var teeTimeList = _reservationService.FindTeeTimes(teeTimeFinder);
            return PartialView(teeTimeList);
        }

        [HttpPost]
        public ActionResult AddMembers(string memberID, int teeTimeID)
        {
            var member = _reservationService.AddMemberToReservation(memberID, teeTimeID,
                User.Identity.Name);

            return Json(member, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetWithMembers()
        {
            return PartialView();
        }

        [HttpGet]
        //Returns list of Reservations
        public ActionResult List()
        {
            var reservations = _reservationService.GetMemberReservations(User.Identity.Name);
            System.Collections.Generic.List<ReservViewModels> models = reservations.Select(s => new ReservViewModels
            {
                MemberFullName = s.MemberFullName,
                TeeTimeStartDate = s.TeeTimeStartDate,
                TeeTimeEndDate = s.TeeTimeEndDate,
                ID = s.ID,
                GolfCourse = s.GolfCourse
            }).ToList();

            return View(models);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var teeTimeWithReservations = _teetimeService.GetWithMembers(id);
            return View(teeTimeWithReservations);
        }

        [HttpGet]
        public ActionResult GetList()
        {
            var reservations = _reservationService.GetMemberReservations(User.Identity.Name);

            var reservationsCalendar = reservations.Select(x => new
            {
                id = x.TeeTimeID,
                title =
                "Reservation For " + x.MemberFullName,
                start = x.TeeTimeStartDate.ToString("s"),
                end = x.TeeTimeEndDate.ToString("s"),
                allDay = false,
                GCourse = x.GolfCourse
            });
            return Json(reservationsCalendar, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMemberReservations()
        {
            var reservations = _reservationService.GetMemberReservations(User.Identity.Name);

            var reservationsCalendar = reservations.Select(x => new
            {
                id = x.TeeTimeID,
                title = "Reservation For " + x.MemberFullName,
                start = x.TeeTimeStartDate.ToString("s"),
                end = x.TeeTimeStartDate.ToString("s"),
                allDay = false,
                GolfCourse = x.GolfCourse
            });
            return Json(reservationsCalendar, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Index()
        //{
        //    var member = _memberService.GetMemberByEmail(User.Identity.Name);

        //    var reservationsForMember = _reservationService.GetReservations(member.ID);

        //    return View(reservationsForMember);
        //}

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    var member = _memberService.GetMemberByEmail(User.Identity.Name);
        //    ViewBag.MemberID = member.ID;
        //    return View();
        //}

        [HttpPost]
        public ActionResult Create(CreateReservationModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var member = _memberService.GetMemberByEmail(User.Identity.Name);

                _reservationService.CreateNormalReservation(inputModel, member.ID);

                return RedirectToAction("Index");

            }
            else
                ModelState.AddModelError("", "unable to save cha. Try again, if the problem persists email system administrator");
            return View(inputModel);
        }



        //[HttpGet]
        //public ActionResult GetWithMembers()
        //{
        //    return PartialView();
        //}
        //[HttpGet]
        //public ViewResult Details(int id)
        //{
        //    var member = _memberService.GetMemberById(id);

        //    var reservationsForMember = _reservationService.GetReservations(member.ID);

        //    return View(reservationsForMember);


        }


    }





using System;
using System.Collections.Generic;
using System.Linq;
using TheBackEndLayer.DbModels;
using System.Web;
using TheBackEndLayer.Services;
using System.Web.Mvc;
using System.Net;
using TheBackEndLayer.ViewModels.Reservation;

namespace BAISTGOLF.Controllers
{
    public class ReservationController : Controller
    {
        private ReservationService reservationService = new ReservationService();
        private MembersService memberService = new MembersService();

        public ActionResult Index()
        {
            var member = memberService.GetuserByEmail(User.Identity.Name);

            var reservationsForMember = reservationService.GetReservations(member.ID);

            return View(reservationsForMember);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var member = memberService.GetuserByEmail(User.Identity.Name);
            ViewBag.MemberID = member.ID;
            return View();
        }

        [HttpPost]
        public void Create(CreateReservationModel inputModel)
        {
            var member = memberService.GetuserByEmail(User.Identity.Name);

            reservationService.CreateReservation(inputModel, member.ID);
        }

        [HttpGet]
        public ActionResult GetWithMembers()
        {
            return PartialView();
        }
    }
}
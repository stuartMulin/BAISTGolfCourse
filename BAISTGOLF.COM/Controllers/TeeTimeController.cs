using TheBackEndLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAISTGOLF.Controllers
{
    public class TeeTimeController : Controller
    {
        private TeeTimeService teeTimeService = new TeeTimeService();
        // GET: TeeTime
        public ActionResult GetListBySearchDate(string searchDate)
        {
            var searchDateObject = Convert.ToDateTime(searchDate);

            var teeTimeList = teeTimeService.GetTeeTimesByDate(searchDateObject);

            return PartialView(teeTimeList);
        }
    }
}

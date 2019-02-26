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
        private readonly ITeeTimesService _teeTimeService;
        public TeeTimeController(ITeeTimesService teeTimeService)
        {
            _teeTimeService = teeTimeService;
        }

        public ActionResult GetwithMembers(int id)
        {
            var teeTime = _teeTimeService.GetWithMembers(id);
            return PartialView(teeTime);

        }
     
    }
}

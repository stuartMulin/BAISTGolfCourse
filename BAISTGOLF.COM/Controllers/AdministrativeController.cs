using System.Web.Mvc;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Services;

namespace BAISTGOLF.Controllers
{
    [System.Web.Http.Authorize]
    public class AdminTasksController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAppService _appService;
        private readonly ITeeTimesService _teetimeSerivice;
        // GET: Administrative
        public AdminTasksController (IEmployeeService employeeService, IAppService appService,ITeeTimesService teetimeService)

        {
            _employeeService = employeeService;
            _appService = appService;
            _teetimeSerivice = teetimeService;


        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Application(int id)
        {
            var applicant = _appService.GetUserDetailChangeStatusByID(id);
            return View(applicant);
        }

        [HttpGet]
        public ActionResult ApplicantRequests()
        {
            var applicants = _employeeService.GetAllNewApplicants();
            return View(applicants);
        }

        [HttpGet]
        public ActionResult Applications(int id)
        {
            var applicant = _appService.GetAllApplicants();
            return View(applicant);
        }
        //[HttpGet]
        //public ActionResult TeetimeRequests()
        //{
        //    var existingRequests = _teetimeSerivice.GetListBySearchDate();
        //    return View(existingRequests);
        //}

        [HttpGet]

        public ActionResult MakeDecision(int id, string option)
        {
            var response = _employeeService.MakeDecision(option, id);

            if (response)
                return Json("You have successfuly approved the applicant", JsonRequestBehavior.AllowGet);
            else
                return Json("You have successfuly rejected the applicant", JsonRequestBehavior.AllowGet);
        }
    }
}
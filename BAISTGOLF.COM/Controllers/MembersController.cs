using System.Linq;
using System.Web.Mvc;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Services;
using System.Web.Security;
using TheBackEndLayer.ViewModels;


namespace BAISTGOLF.Controllers
{
    public class MembersController : Controller

    {
        private MembersService _Mservice = new MembersService();

        public ActionResult Detail(int id)
        {
            var member = _Mservice.GetuserById(id);

            return View(member);
        }

        [HttpGet]
        public ActionResult GetByEmail(string searchEmail)
        {
            var member = _Mservice.GetuserByEmailSearch(searchEmail);

            return Json(member, JsonRequestBehavior.AllowGet);
        }
    }
    
}



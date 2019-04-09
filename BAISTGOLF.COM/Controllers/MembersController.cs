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
        //private MemberService _Mservice = new MemberService();
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public ActionResult Detail(int id)
        {
            var member = _memberService.GetMemberById(id);

            return View(member);
        }

        [HttpGet]
        public ActionResult GetByEmail(string searchEmail)
        {
            var member = _memberService.GetMemberByEmail(searchEmail);

            return Json(member, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberAccount(string id)
        {
            var memberViewModel = _memberService.GetMemberByMembershipID(id);

            FormsAuthentication.SetAuthCookie(memberViewModel.EmailAddress, true);

            return View(memberViewModel);

        }
    }

}

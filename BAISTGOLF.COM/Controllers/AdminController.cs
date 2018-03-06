using System.Web.Mvc;
using TheBackEndLayer.Services;

namespace BAISTGOLF.Controllers
{
    public class AdminController : Controller
    {
        private EmployeeService employeeService = new EmployeeService();

        public ActionResult Detail(int id)
        {
            var employee = employeeService.GetuserById(id);
            return View(employee);
        }
        }
    }

                
           
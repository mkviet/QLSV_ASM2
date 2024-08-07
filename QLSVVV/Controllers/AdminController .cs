using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace QLSVVV.Controllers
{

    /*[Authorize(Roles = "Admin")]
    [Authorize(Roles = "Student")]
    [Authorize(Roles = "Teacher")]*/
    [Authorize(Roles = "Admin")]
    [AllowAnonymous]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageStudent()
        {
            return View();
        }

        public IActionResult ManageTeacher()
        {
            return View();
        }

        public IActionResult ManageCourse()
        {
            return View();
        }

        public IActionResult ManageClass()
        {
            return View();
        }
    }
}

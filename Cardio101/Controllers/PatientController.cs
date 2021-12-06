using Microsoft.AspNetCore.Mvc;

namespace Cardio101.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Hello " + "moe";
            return View();
        }
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
    
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyIdentityWeb.Controllers
{

    public class ConferenceController : Controller
    {

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin,organizer")]
        public IActionResult Info()
        {
            return View();
        }
    }
}

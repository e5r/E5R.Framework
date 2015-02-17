using E5R.Framework.Security.Auth;
using Microsoft.AspNet.Mvc;

namespace E5R.Framework.Security.Auth.Web.Controllers
{
    public class HomeController : Controller
    {
        [Protection(ProtectionLevel.Public)]
        public IActionResult Index()
        {
            return View();
        }

        public string Nothing()
        {
            return "Nothing";
        }

        [Protection(ProtectionLevel.Protected)]
        public IActionResult ProtectedPage()
        {
            return View();
        }

        [Protection(ProtectionLevel.Private, new[]
        {   "namespace.sample.permission0",
            "namespace.sample.permission1",
        
         })]
        public IActionResult PrivatePage()
        {
            return View();
        }
    }
}

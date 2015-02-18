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
        public string ProtectedPage()
        {
            return "ProtectedPage";
        }

        [Protection(ProtectionLevel.Private, new[]
        {   "namespace.sample.permission0",
            "namespace.sample.permission1",
        
         })]
        public string PrivatePage()
        {
            return "PrivatePage";
        }

        [Protection(ProtectionLevel.Private, new[] { "permission1", "permission5", "permission3"})]
        public string PrivatePageAllowed()
        {
            return "PrivatePageAllowed";
        }
    }
}

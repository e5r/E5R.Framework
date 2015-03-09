using Microsoft.AspNet.Mvc;

namespace E5R.Framework.Security.Auth.Web.Controllers
{
    public class HomeController : Controller
    {
        [Public]
        public IActionResult Index()
        {
            return View();
        }

        [Protection(ProtectionLevel.Public)]
        public IActionResult Index2()
        {
            return View("Index");
        }

        public string Nothing()
        {
            return "Nothing";
        }

        [Protected]
        public string ProtectedPage()
        {
            return "ProtectedPage";
        }

        [Private(requiredPermissions: new[] {
           "namespace.sample.permission0",
           "namespace.sample.permission1"
         })]
        public string PrivatePage()
        {
            return "PrivatePage";
        }

        [Private(new[] { "permission1", "permission5", "permission3"})]
        public string PrivatePageAllowed()
        {
            return "PrivatePageAllowed";
        }
    }
}

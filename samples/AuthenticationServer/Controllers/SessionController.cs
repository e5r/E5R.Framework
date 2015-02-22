using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace AuthenticationServer.Controllers.Controllers
{
    [Route("session")]
    public class SessionController : Controller
    {
        [HttpPost]
        public void CreateSession()
        {
        }
    }
}

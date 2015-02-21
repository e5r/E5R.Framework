using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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

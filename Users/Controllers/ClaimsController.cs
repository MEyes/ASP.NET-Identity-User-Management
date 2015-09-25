using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Users.Infrastructure;

namespace Users.Controllers
{
    public class ClaimsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity==null)
            {
                return View("Error", new string[] {"未找到声明"});
            }
            else
            {
                return View(claimsIdentity.Claims);
            }
        }

        //[ClaimsAccess(Issuer = "RemoteClaims", ClaimType = ClaimTypes.PostalCode, Value = "上海")]
        [Authorize(Roles = "DCStaff")]
        public string OtherAction()
        {
            return "这是一个受保护的Action";
        }
	}
}
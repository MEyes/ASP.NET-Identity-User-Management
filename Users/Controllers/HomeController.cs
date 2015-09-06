using System.Collections.Generic;
using System.Web.Mvc;

namespace Users.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict
                = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("用户", HttpContext.User.Identity.Name);
            dict.Add("是否身份验证通过", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("身份验证类型", HttpContext.User.Identity.AuthenticationType);
            dict.Add("是否隶属于Administrator", HttpContext.User.IsInRole("Administrator"));
            return dict;
        }
    }
}
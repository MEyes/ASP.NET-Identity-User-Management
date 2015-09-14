using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Users.Infrastructure;
using Users.Models;

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

       [Authorize]
        public ActionResult UserProps()
       {
           return View(CurrentUser);
       }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UserProps(Cities city)
        {
            AppUser user = CurrentUser;
            user.City = city;
            user.SetCountryFromCity(city);
            await UserManager.UpdateAsync(user);
            return View(user);
        }
        private AppUser CurrentUser
        {
            get { return UserManager.FindByName(HttpContext.User.Identity.Name); }
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
    }
}
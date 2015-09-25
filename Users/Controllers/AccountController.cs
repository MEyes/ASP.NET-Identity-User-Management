using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //如果登录用户已经Authenticated，提示请勿重复登录

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] {"您已经登录！"});
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        public ActionResult LogOut()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model,string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(model.Name, model.Password);
                if (user==null)
                {
                    ModelState.AddModelError("","无效的用户名或密码");
                }
                else
                {
                    var claimsIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    claimsIdentity.AddClaims(LocationClaimsProvider.GetClaims(claimsIdentity));
                    claimsIdentity.AddClaims(ClaimsRoles.CreateRolesFromClaims(claimsIdentity));
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties {IsPersistent = false}, claimsIdentity);

                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GoogleLogin(string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback",
                new { returnUrl = returnUrl })
            };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }
        /// <summary>
        /// Google登陆成功后（即授权成功）回掉此Action
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthManager.GetExternalLoginInfoAsync();
            AppUser user = await UserManager.FindAsync(loginInfo.Login);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,
                    City = Cities.Shanghai,
                    Country = Countries.China
                };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
            }
            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);
            ident.AddClaims(loginInfo.ExternalIdentity.Claims);
            AuthManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, ident);
            return Redirect(returnUrl ?? "/");
        }
        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Users.Infrastructure;

namespace Users
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext 内幕：
            //1.使用app.Use方法将IdentityFactoryMiddleware和参数callback回掉函数注册到Owin Pipeline中
            //app.Use(typeof(IdentityFactoryMiddleware<T, IdentityFactoryOptions<T>>), args);
            //2.当IdentityFactoryMiddleware中间件被Invoke执行时，执行callback回掉函数，返回具体实例Instance
            //TResult instance = ((IdentityFactoryMiddleware<TResult, TOptions>) this).Options.Provider.Create(((IdentityFactoryMiddleware<TResult, TOptions>) this).Options, context);
            //3.将返回的实例存储在Owin Context中
            //context.Set<TResult>(instance);

            app.CreatePerOwinContext(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            //app.UseCookieAuthentication 内幕：
            //1.将 CookieAuthenticationMiddleware 中间件注册到OWIN Pipeline中
            //app.Use(typeof(CookieAuthenticationMiddleware), app, options);
            //2.前面添加的CookieAuthenticationMiddleware指定在 ASP.NET 集成管道（ASP.NET integrated pipeline）的AuthenticateRequest阶段执行
            //var stage=PipelineStage.Authenticate;
            //app.UseStageMarker(stage);
            //3.当调用（Invoke）此Middleware时，将调用CreateHandler方法返回CookieAuthenticationHandler对象
            //AuthenticationHandler<TOptions> handler = CreateHandler();
            //4.CookieAuthenticationHandler对象包含AuthenticateCoreAsync、ApplyResponseGrantAsync方法
            //AuthenticateCoreAsync：read && validate cookie，然后通过AddUserIdentity方法创建ClaimsPrincipal对象并添加到到Owin环境字典中，可以通过OwinContext对象Request.User可以获取当前用户
            //5.var newClaimsPrincipal = new ClaimsPrincipal(identity);
            //5._context.Request.User = newClaimsPrincipal;
            //6.ApplyResponseGrantAsync：Response cookie

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
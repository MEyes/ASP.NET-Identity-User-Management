using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Users.Models {
    /// <summary>
    /// 自定义的User Class继承IdentityUser，在此扩展自定义User属性
    /// </summary>
    public class AppUser : IdentityUser {
        // 在此添加额外属性
    }
}

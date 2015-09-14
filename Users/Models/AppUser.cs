using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Users.Models {
    /// <summary>
    /// 自定义的User Class继承IdentityUser，在此扩展自定义User属性
    /// </summary>
    public class AppUser : IdentityUser 
    {
        // 在此添加额外属性
        public Countries Country { get; set; }
        public Cities City { get; set; }

        public void SetCountryFromCity(Cities city)
        {
            switch (city)
            {
                case Cities.上海:
                case Cities.杭州:
                    Country=Countries.中国;
                    break;
                case Cities.纽约:
                case Cities.洛杉矶:
                    Country=Countries.美国;
                    break;
                  case Cities.东京:
                    Country=Countries.日本;
                    break;
                default:
                    Country=Countries.无;
                    break;
            }
        }

    }

    public enum Countries
    {
        中国,
        美国,
        日本,
        无
    }
    public enum Cities
    {
        上海,
        杭州,
        纽约,
        洛杉矶,
        东京
    }
}

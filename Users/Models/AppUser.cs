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
                case Cities.Shanghai:
                case Cities.Hangzhou:
                    Country=Countries.China;
                    break;
                case Cities.NewYork:
                    Country=Countries.USA;
                    break;
                  case Cities.Tokyo:
                    Country=Countries.Japan;
                    break;
                default:
                    Country=Countries.None;
                    break;
            }
        }

    }

    public enum Countries
    {
        China,
        USA,
        Japan,
        None
    }
    public enum Cities
    {
        Shanghai,
        Hangzhou,
        NewYork,
        Tokyo
    }
}

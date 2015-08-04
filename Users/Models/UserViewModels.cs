using System.ComponentModel.DataAnnotations;

namespace Users.Models {

   /// <summary>
   /// 用户创建ViewModel
   /// </summary>
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

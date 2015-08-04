using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Users.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);
            if (password.Contains("12345"))
            {
                List<string> errors = result.Errors.ToList();
                errors.Add("密码不能包含连续数字");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}

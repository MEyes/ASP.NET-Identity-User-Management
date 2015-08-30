using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
    public class RoleModificationModel
    {
        public string RoleName { get; set; }
        public string[] IDsToAdd { get; set; }
        public string[] IDsToDelete { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    public class UserGroupApplicationUser
    {
        public int UserGroupID { get; set; }
        public string ApplicationUserID { get; set; }

        public UserGroup UserGroup { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

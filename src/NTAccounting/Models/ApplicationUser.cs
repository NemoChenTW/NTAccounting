﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NTAccounting.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        // 透過UserGroupApplicationUser 設定與 UserGroup 的多對多關係
        public List<UserGroupApplicationUser> UserGroupApplicationUser { get; set; }
    }
}

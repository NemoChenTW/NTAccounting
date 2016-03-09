using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTAccounting.Models
{
    public class UserGroup
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "群組名稱")]
        public string Name { get; set; }


        // 設定與ApplicationUser的關聯
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}

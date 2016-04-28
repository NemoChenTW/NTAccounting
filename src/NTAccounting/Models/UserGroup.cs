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


        // 透過UserGroupApplicationUser 設定與 設定與ApplicationUser的關聯 的多對多關係
        public List<UserGroupApplicationUser> UserGroupApplicationUser { get; set; }

        // FinancialAccount Foreignkey關係
        public virtual ICollection<FinancialAccount> FinancialAccounts { get; set; }
    }
}

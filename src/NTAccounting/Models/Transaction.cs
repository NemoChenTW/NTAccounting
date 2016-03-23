using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    public class Transaction
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "交易名稱")]
        public string Name { get; set; }

        [Display(Name = "交易金額")]
        public int Amount { get; set; }

        [Display(Name = "交易時間")]
        public DateTime Time { get; set; }

        //Foreign key for SubTransactionCategory
        [Display(Name = "交易子類別")]
        public int SubTransactionCategoryID { get; set; }
        [ForeignKey("SubTransactionCategoryID")]
        public SubTransactionCategory SubTransactionCategory { get; set; }

        //Foreign key for UserGroup
        [Display(Name = "使用者群組")]
        public int UserGroupID { get; set; }
        [ForeignKey("UserGroupID")]
        public UserGroup UserGroup { get; set; }


    }
}

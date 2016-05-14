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
        [DataType(DataType.Currency)]
        public int Amount { get; set; }

        [Display(Name = "交易時間")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        //Foreign key for SubTransactionCategory
        [Display(Name = "交易子類別")]
        public int SubTransactionCategoryID { get; set; }
        [ForeignKey("SubTransactionCategoryID")]
        public SubTransactionCategory SubTransactionCategory { get; set; }

        //Foreign key for FinancialAccount
        [Display(Name = "交易帳戶")]
        public int FinancialAccountID { get; set; }
        [ForeignKey("FinancialAccountID")]
        public FinancialAccount FinancialAccount { get; set; }

        //目標帳戶 (轉帳交易使用)
        public int? TargetFinancialAccountID { get; set; }
        [ForeignKey("TargetFinancialAccountID")]
        public FinancialAccount TargetFinancialAccount { get; set; }
    }
}

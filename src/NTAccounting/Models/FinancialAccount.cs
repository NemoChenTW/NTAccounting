using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    public class FinancialAccount
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "帳戶名稱")]
        public string Name { get; set; }

        [Display(Name = "帳戶餘額")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }


        //Foreign key for FinancialAccountType
        [Display(Name = "帳戶類型")]
        public int TypeID { get; set; }

        [ForeignKey("TypeID")]
        public virtual FinancialAccountType FinancialAccountType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    public class SubTransactionCategory
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "交易子類別名稱")]
        public string Name { get; set; }

        //Foreign key for TransactionMainCategory
        [Display(Name = "交易主類別")]
        public int MainCategoryID { get; set; }

        [ForeignKey("MainCategoryID")]
        public virtual MainTransactionCategory MainTransactionCategory { get; set; }
    }
}

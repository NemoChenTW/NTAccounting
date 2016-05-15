using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    public class MainTransactionCategory : TransactionCategory
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "交易主類別名稱")]
        public string Name { get; set; }

        public virtual ICollection<SubTransactionCategory> SubTransactionCategories { get; set; }
    }
}

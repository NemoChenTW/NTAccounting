using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    public abstract class TransactionCategory
    {
        public enum TransactionCategoryType
        {
            Expense,
            Income,
            Transfer
        }

        [Display(Name = "交易類型")]
        public TransactionCategoryType TransactionType { get; set; }
    }
}

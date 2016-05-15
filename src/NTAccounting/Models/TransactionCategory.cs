using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public string TransactionTypeName
        {
            get
            { 
                switch(this.TransactionType)
                {
                    case TransactionCategoryType.Expense:
                        return "支出";
                    case TransactionCategoryType.Income:
                        return "收入";
                    case TransactionCategoryType.Transfer:
                        return "轉帳";
                    default:
                        return "錯誤的交易類型";
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace NTAccounting.Models
{
    public class Transaction : ICalcTransaction
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "交易")]
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



        // 新增交易時使用, 會根據交易類型增加或減少帳戶餘額.
        public void CreateTransaction(ApplicationDbContext context)
        {
            TransactionCategory.TransactionCategoryType transType = GetTransactionCategoryType(context);

            var accounts = context.FinancialAccount;
            var account = accounts.Single(ac => ac.ID == this.FinancialAccountID);

            if (transType == TransactionCategory.TransactionCategoryType.Expense)
            {
                account.Amount -= Amount;
            }
            else if(transType == TransactionCategory.TransactionCategoryType.Income)
            {
                account.Amount += Amount;
            }
            else if(transType == TransactionCategory.TransactionCategoryType.Transfer)
            {
                var targetAccount = accounts.Single(ac => ac.ID == this.TargetFinancialAccountID);

                account.Amount -= Amount;
                targetAccount.Amount += Amount;
            }
        }

        // 更新交易時使用, 會修改後的交易金額調整帳戶餘額 (目前默認不可修改交易類型)
        public void UpdateTransaction(ApplicationDbContext context)
        {
            TransactionCategory.TransactionCategoryType transType = GetTransactionCategoryType(context);

            var accounts = context.FinancialAccount;
            var account = accounts.Single(ac => ac.ID == this.FinancialAccountID);

            var oldTransactionAmount = context.Transaction.AsNoTracking().Single(t => t.ID == ID).Amount;
 
            if (transType == TransactionCategory.TransactionCategoryType.Expense)
            {
                account.Amount += oldTransactionAmount;
                account.Amount -= Amount;
            }
            else if (transType == TransactionCategory.TransactionCategoryType.Income)
            {
                account.Amount -= oldTransactionAmount;
                account.Amount += Amount;
            }
            else if (transType == TransactionCategory.TransactionCategoryType.Transfer)
            {
                var targetAccount = accounts.Single(ac => ac.ID == this.TargetFinancialAccountID);

                account.Amount += oldTransactionAmount;
                account.Amount -= Amount;

                targetAccount.Amount -= oldTransactionAmount;
                targetAccount.Amount += Amount;
            }
        }

        // 刪除交易時使用, 會根據交易類型回復帳戶餘額.
        public void DeleteTransaction(ApplicationDbContext context)
        {
            TransactionCategory.TransactionCategoryType transType = GetTransactionCategoryType(context);

            var accounts = context.FinancialAccount;
            var account = accounts.Single(ac => ac.ID == this.FinancialAccountID);

            if (transType == TransactionCategory.TransactionCategoryType.Expense)
            {
                account.Amount += Amount;
            }
            else if (transType == TransactionCategory.TransactionCategoryType.Income)
            {
                account.Amount -= Amount;
            }
            else if (transType == TransactionCategory.TransactionCategoryType.Transfer)
            {
                var targetAccount = accounts.Single(ac => ac.ID == this.TargetFinancialAccountID);

                account.Amount += Amount;
                targetAccount.Amount -= Amount;
            }
        }

        private TransactionCategory.TransactionCategoryType GetTransactionCategoryType(ApplicationDbContext context)
        {
            var SubCate = context.SubTransactionCategory.Single(s => s.ID == SubTransactionCategoryID);
            var MainCate = context.MainTransactionCategory.Single(m => m.ID == SubCate.MainCategoryID);

            return MainCate.TransactionType;
        }
    }
}

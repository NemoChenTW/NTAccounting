﻿using System;
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

        [Display (Name = "初始金額")]
        [DataType(DataType.Currency)]
        public decimal InitialAmount { get; set; }

        [Display(Name = "帳戶餘額")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }


        //Foreign key for FinancialAccountType
        [Display(Name = "帳戶類型")]
        public int TypeID { get; set; }
        [ForeignKey("TypeID")]
        public virtual FinancialAccountType FinancialAccountType { get; set; }


        //Foreign key for UserGroup
        [Display(Name = "帳戶隸屬群組")]
        public int UserGroupID { get; set; }
        [ForeignKey("UserGroupID")]
        public virtual UserGroup UserGroup { get; set; }


        // Transaction Foreignkey關係
        public virtual ICollection<Transaction> Transactions { get; set; }

        // 轉帳交易目標帳戶的Foreignkey 關係
        public virtual ICollection<Transaction> TransferTransactions { get; set; }
    }
}

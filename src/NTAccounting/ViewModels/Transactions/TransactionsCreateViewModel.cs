﻿using Microsoft.AspNet.Mvc.Rendering;
using NTAccounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.ViewModels.Transactions
{
    public class TransactionsCreateViewModel
    {
        public IEnumerable<SelectListItem> MainTransactionCategoryCollection { get; set; }

        public IEnumerable<SelectListItem> SubTransactionCategoryCollection { get; set; }

        public IEnumerable<SelectListItem> UserGroupCollection { get; set; }

        public IEnumerable<SelectListItem> FinancialAccountCollection { get; set; }

        public string UserGroupDisplayName { get; set; }

        public Transaction Transaction { get; set; }

        public TransactionCategory.TransactionCategoryType TransactionCategoryType { get; set; }
    }
}

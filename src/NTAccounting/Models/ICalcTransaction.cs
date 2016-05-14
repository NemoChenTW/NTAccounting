using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    interface ICalcTransaction
    {
        void CreateTransaction(ApplicationDbContext context);

        void UpdateTransaction(ApplicationDbContext context);

        void DeleteTransaction(ApplicationDbContext context);
    }
}

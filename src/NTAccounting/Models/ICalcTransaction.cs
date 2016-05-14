using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    interface ICalcTransaction
    {
        void CreateTransaction();

        void UpdateTransaction();

        void DeleteTransaction();
    }
}

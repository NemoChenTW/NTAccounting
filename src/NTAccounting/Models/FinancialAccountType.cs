using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.Models
{
    public class FinancialAccountType
    {
        public int ID { get; set; }

        [Display(Name = "帳戶類型")]
        public string Type { get; set; }
    }
}

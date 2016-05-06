using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTAccounting.ViewModels.Manage
{
    public class ChangeRepresentativeGroupViewModel
    {
        [Required]
        [Display(Name = "使用者群組")]
        public int RepresentativeGroupID;
    }
}

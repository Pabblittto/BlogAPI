using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
        public string OldPassword {get;set; }

    }
}

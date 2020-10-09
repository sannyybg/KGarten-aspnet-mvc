using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kindergarten3.ViewModel
{
    public class ChangePassViewModel
    {
        [DisplayName("ძველი პაროლი")]
        public string CurrentPassword { get; set; }

        [DisplayName("ახალი პაროლი")]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string NewPassword { get; set; }

        [DisplayName("გაიმეორეთ პაროლი")]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string ConfirmPassword { get; set; }
    }
}
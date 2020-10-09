using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "მინიმუმ 8 სიმბოლო")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "")]
        [DisplayName("პაროლი")]
        [MinLength(8, ErrorMessage = "პაროლი უნდა იყოს მინიმუმ 8 სიმბოლო")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "მიუთითეთ სწორი იმეილი")]
        public string Email { get; set; }

        [Required]
        [DisplayName("სახელი")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("გვარი")]
        public string LastName { get; set; }
        
        [DisplayName("ფოტო")]
        public string PhotoURL { get; set; }

        public bool isActive { get; set; } = false;

        public List<Role> Roles { get; set; }


    }
}
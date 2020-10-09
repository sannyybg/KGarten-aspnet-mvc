using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kindergarten3.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [MinLength(8, ErrorMessage = "Username უნდა იყოს მინიმუმ 8 სიმბოლო")]
        public string Username { get; set; }

        [DataType(DataType.Password, ErrorMessage = "")]
        [MinLength(8, ErrorMessage = "პაროლი უნდა იყოს მინიმუმ 8 სიმბოლო")]
        [DisplayName("პაროლი")]
        public string Password { get; set; }

        [Required]
        [DisplayName("სახელი")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("გვარი")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("დაბადების თარიღი")]
        public DateTime BirthDate { get; set; }

        [Required]
        [DisplayName("პირადი ნომერი")]
        public int PersonalCardId { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "მიუთითეთ სწორი იმეილი")]
        public string Email { get; set; }

        [Required]
        [DisplayName("მშობლის პირადი ნომერი")]
        public int ParentPersonalId { get; set; }

        public bool isActive { get; set; } = false;


        public List<Role> Roles { get; set; }





    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Task_Project.Models.Entities
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "This field is required ")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required ")]
        [MaxLength(50)]
        public string Lastname { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "This field is required ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Age Invalid")]
        public int Age { get; set; }

        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "This field is required ")]
        [MaxLength(50)]
        [MinLength(6)]
        [Index(IsUnique = true)]
        public string Login { get; set; }

        [Required(ErrorMessage = "This field is required ")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsBlocked { get; set; }

       
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.ViewModels.Account
{
    public class RegisterViewModel
    {
        #region ============== New Edited ============== 

        [Required]
        [RegularExpression(pattern: @"\w+", ErrorMessage = "Please enter alphanumeric characters for this one.")]
        [Display(Name = "First Name")]
        [StringLength(25, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(pattern: @"\w+", ErrorMessage = "Please enter alphanumeric characters for this one.")]
        [Display(Name = "Last Name")]
        [StringLength(25, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(pattern: @"\d+", ErrorMessage = "Please enter digits.")]
        public int Age { get; set; }

        [Required]
        [RegularExpression(pattern: @"\d+", ErrorMessage = "Please enter digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }

        #endregion

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

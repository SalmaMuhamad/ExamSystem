using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizbeePlus.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }


    public class RegisterViewModel
    {
        //FullName
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        //Email
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Password
        [StringLength(100, ErrorMessage = "The {0} must be at least {8} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //ConfirmPassword
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //NationalID
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        [StringLength(100, ErrorMessage = "The {0} must be  {2} characters long.", MinimumLength = 14)]
        public string NationalID { get; set; }

        //MobileNum
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        [StringLength(100, ErrorMessage = "The {0} must be  {2} characters long.", MinimumLength = 11)]
        public string MobileNumber { get; set; }

        //wzara/Geha
        public string Elgeha { get; set; }

        //elQeta3
        public string Sector { get; set; }

        //Wazefa
        public string JobTitle { get; set; }

        //address
        public string Address { get; set; }

        //University
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets  allowed.")]
        public string University { get; set; }

        //Faculty
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets  allowed.")]
        public string Faculty { get; set; }


        //Grade
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets  allowed.")]
        public string Grade { get; set; }

        //GradYear
        public string GradYear { get; set; }
        public DateTime BirthDate { get; set; }



    }

    public class ResetPasswordViewModel
    {
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

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

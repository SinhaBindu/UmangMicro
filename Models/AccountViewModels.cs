﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UmangMicro.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
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
        public string Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        //[EmailAddress]
        public string Email { get; set; }
      
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        public string Id { get; set; }
      //  [Required]
        //[EmailAddress]
        [Display(Name = "User Name")]
        public string Email { get; set; }

       // [Required]
       // [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
      //  [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Mobile No")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Mobile No Confirmed")]
        public string PhoneNumberConfirmed { get; set; }
        [Display(Name = "School Name")]
        public int? SchoolId { get; set; }
        //[Display(Name = "State")]
        //public int? StateId { get; set; }
        [Display(Name = "District")]
        public int? DistrictId { get; set; }
        [Display(Name = "Block")]
        public int? BlockId { get; set; }
        //[Display(Name = "Cluster")]
        //public int? ClusterId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Role")]
        public string Role { get; set; }
        public string CreatedBY { get; set; }
        public DateTime CreatedDt { get; set; }
    }
    public class UserEditViewModel
    {
        public string Id { get; set; }
       // [Required]
       // [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Mobile No")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Mobile No Confirmed")]
        public string PhoneNumberConfirmed { get; set; }
        //[Display(Name = "State")]
        //public int? StateId { get; set; }
        [Display(Name = "District")]
        public int? DistrictId { get; set; }
        [Display(Name = "Block")]
        public int? BlockId { get; set; }
        //[Display(Name = "Cluster")]
        //public int? ClusterId { get; set; }

        [Display(Name = "School Name")]
        public int? SchoolId { get; set; }
      
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
    public class UserViewModel
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string DistrictId { get; set; }
        public string District { get; set; }
        public string BlockId { get; set; }
        public string Block { get; set; }
        public string ClusterId { get; set; }
        public string Cluster { get; set; }
        public string SchoolId { get; set; }
        public string SchoolN { get; set; }
        public string Role { get; set; }
        public string RoleId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LockoutEnabled { get; set; }
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

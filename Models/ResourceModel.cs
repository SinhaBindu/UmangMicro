using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class ResourceModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Designation")]
        [Required]
        public string Designation { get; set; }
        [Display (Name = "Designation Other")]
        public string Designation_Other { get; set; }
        [Display(Name = "Organisation")]
        //[Required]
        public string Organisation { get; set; }
        [EmailAddress]
        [Display(Name = "Email ID")]
        //[Required]
        public string Email { get; set; }
        [Display(Name = "Contact No")]
        [Required]
        public string ContactNo { get; set; }
        [Display(Name = "Age")]
        [Required]
        public string Age { get; set; }
        [Display(Name = "State")]
        [Required]
        public string State { get; set; }
        [Display(Name = "District")]
        [Required]
        public string District { get; set; }
        [Display(Name = "Download Resource File")]
        [Required]
        public List<string> ResourceDownLoad { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }

   
}
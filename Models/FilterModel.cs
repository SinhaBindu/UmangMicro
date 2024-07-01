using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class FilterModel
    {
        public string FD { get; set; }
        public string TD { get; set; }
        [Display(Name = "District")]
        public string DistrictId { get; set; }
        [Display(Name = "Block")]
        public string BlockId { get; set; }
        [Display(Name = "School")]
        public string SchoolId { get; set; }
        [Display(Name = "Training")]
        public Nullable<int> Trainingtype { get; set; }
        [Display(Name = "Round")]
        public Nullable<int> Round { get; set; }
        [Display(Name = "Name of Teacher")]
        public string Nameofteacher { get; set; }
        [Display(Name = "Date")]
        public Nullable<System.DateTime> Date { get; set; }
        [Display(Name = "No. of Teacher Trained")]
        public Nullable<int> Noofteachertrained { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }
    }
    public class FilterCanReg
    {
        public string FD { get; set; }
        public string TD { get; set; }
        [Display(Name = "District")]
        public string DistrictId { get; set; }
        [Display(Name = "Block")]
        public string BlockId { get; set; }
        [Display(Name = "School")]
        public string SchoolId { get; set; }
    }
}
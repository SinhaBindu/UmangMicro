using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class ModularSModel
    {
        public int Id { get; set; }
        [Display(Name = "Cohort")]
        public int Cohort { get; set; }
        public Nullable<System.DateTime> ConductedDate { get; set; }
        public Nullable<int> Reg_fk_Id { get; set; }
        [Display (Name = "District")]
        public Nullable<int> DistrictId { get; set; }
        [Display(Name = "Block")]
        public Nullable<int> BlockId { get; set; }
        [Display(Name = "School")]
        public Nullable<int> SchoolId { get; set; }
        [Display(Name = "Calss")]
        public Nullable<int> CalssId { get; set; }
        [Display(Name = "No of Student")]
        public Nullable<int> NoofStudent { get; set; }
        public Nullable<int> Session { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }
        public List<ModularChldModel> ModularChldModel { get; set; }
    }
    public class ModularChldModel
    {
        public int Id { get; set; }
        public Nullable<int> Modular_fk_Id { get; set; }
        public Nullable<int> CalssId { get; set; }
        public Nullable<int> NoOfStudent { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }
        public Nullable<bool> IsCheck { get; set; }


    }
}
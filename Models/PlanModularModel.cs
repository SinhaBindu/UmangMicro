using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class PlanModularModel
    {
        public int Id { get; set; }
        [Display(Name = "Cohort")]
        public int Cohort { get; set; }
        [Display(Name = "Plan Date")]
        public Nullable<System.DateTime> ConductedDate { get; set; }
        public Nullable<int> Reg_fk_Id { get; set; }
        [Display(Name = "District")]
        public Nullable<int> DistrictId { get; set; }
        [Display(Name = "Block")]
        public Nullable<int> BlockId { get; set; }
        [Display(Name = "School")]
        public Nullable<int> SchoolId { get; set; }
        [Display(Name = "Calss")]
        public Nullable<int> CalssId { get; set; }
        [Display(Name = "No of Student")]
        public Nullable<int> NoofStudent { get; set; }
        [Display(Name = "Task")]
        public Nullable<int> TaskType { get; set; }
        [Display(Name = "Class")]
        public string ClassMLT { get; set; }
        [Display(Name = "Session Type")]
        public Nullable<int> SessionType { get; set; }
        [Display(Name = "Others")]
        public string SessionInput { get; set; }
        [Display(Name = "Courses")]
        public Nullable<int> coursemaster { get; set; }
        [Display(Name = "Career")]
        public Nullable<int> CareerMaster { get; set; }


        [Display(Name = "Session")]
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
    public class PlanModular
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
        public Nullable<int> TaskType { get; set; }
        public Nullable<int> ClassMLT { get; set; }
        public Nullable<int> SessionType { get; set; }
        public string SessionInput { get; set; }
        public Nullable<System.DateTime> ConductedDate { get; set; }


    }
}
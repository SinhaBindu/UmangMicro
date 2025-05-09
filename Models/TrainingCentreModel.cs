﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class TrainingCentreModel
    {
        public TrainingCentreModel()
        {
            Id = 0;
        }
        public int Id { get; set; }
        [Display(Name = "District")]
        public Nullable<int> DistrictId { get; set; }
        [Display(Name = "Block")]
        public Nullable<int> BlockId { get; set; }
        [Display(Name = "School")]
        public Nullable<int> SchoolId { get; set; }
        [Display(Name = "Training")]
        public Nullable<int> Trainingtype { get; set; }
        [Display(Name = "Round")]
        public Nullable<int> Round { get; set; }
        [Display(Name = "Cohort")]
        public string Cohortmlt { get; set; }
        //[Display(Name = "Session Mlt")]
        //public List<string> sessionMlt { get; set; } = new List<string>();
        [Display(Name = "Session")]
        public string sessionMlt { get; set; }
        [Display(Name = "Name of Teacher")]
        public string NameofteacherMlt { get; set; }
        [Display(Name = "School")]
        public string SchoolMlt { get; set; }
        [Display(Name = "District")]
        public string DistrictMlt { get; set; }
        [Display(Name = "Name of Teacher")]
        public string Nameofteacher { get; set; }
        [Display(Name = "Name of Teacher Trained")]
        public string Comment { get; set; }
        [Display(Name = "Date")]
        public Nullable<System.DateTime> Date { get; set; }
        [Display(Name = "No. of Teacher Trained")]
        public Nullable<int> Noofteachertrained { get; set; }
        public string TeacherIds { get; set; }
        public string DistrictIds { get; set; }
        public string SchoolIds { get; set; }
        public string sessionIds { get; set; }
        public string CohortIds { get; set; }


        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }
    }

    public class Training
    {
        public int Id { get; set; }
        public Nullable<int> Trainingtype { get; set; }
        public Nullable<int> Round { get; set; }
        public string Nameofteacher { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }
    }
}
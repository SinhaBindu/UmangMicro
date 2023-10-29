using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class CHModel
    {
        public CHModel()
        {
            Id = 0;
        }
        public string CountdownStart { get; set; }
        public string CountdownEnd { get; set; }
        public string TypeCase { get; set; }
        public string Searchtxt { get; set; }
        [Display(Name = "Date of counselling")]
        public DateTime? DOC { get; set; }
        public Nullable<System.Guid> GudID { get; set; }
        public int Id { get; set; }
        [Display (Name ="Case ID")]
        [Required]
        public string CaseID { get; set; }
        [Display(Name = "Class Name")]
        [Required]
        public string ClassId { get; set; }
        [Display(Name = "Type of counsellor")]
        [Required]
        public string TypeCounsellor { get; set; }
        [Display(Name = "Type of Query")]
        [Required]
        public string TypeQuery { get; set; }
        [Display(Name = "Please write your query using key words like stream of education interested to pursue, particular subject around which you seek information, interest, etc.")]
        public string KeyWords { get; set; }
        [Display(Name = "How long do would you be interested to study after 10th standard (if counselee is studying under 10th)")]
        public string Study10th { get; set; }
        [Display(Name = "How long do would you be interested to study after 12th standard (if counselee is studying in 11th and 12th)")]
        public string Study12th { get; set; }
        [Display(Name = "Which Subject is your favourite")]
        public string Subject { get; set; }
        [Display(Name = "Tools used for counselling")]
        public string Counselling { get; set; }
        [Display(Name = "Conducted psychometric Test")]
        public string IsPsychometric { get; set; }
        [Display(Name = "Result of Psychometric test")]
        public string Psychometric { get; set; }
        [Display(Name = "Areas of improvement")]
        public string AreasImprovement { get; set; }
        [Display(Name = "Counselling advice/ recommendations")]
        public string Recommendation { get; set; }
        [Display(Name = "Follow up")]
        public string IsFollow { get; set; }
        [Display(Name = "Month")]
        public string FM { get; set; }
        [Display(Name = "Year")]
        public string FY { get; set; }
        [Display(Name = "Is the career goal clear to counseleee after the session")]
        public string IsGoalClear { get; set; }
        public DateTime StratTime { get; set; }
        public DateTime EndTime { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public List<CH_Psychometric_Model> CH_PHY_model { get; set; }
        public List<CH_CourseD_Model> CH_CD_model { get; set; }
        public List<CH_SkillT_Model> CH_SkillT_model { get; set; }
        public List<CH_Scholarship_Model> CH_SSP_model { get; set; }
        public List<CH_Scheme_Model> CH_SM_model { get; set; }
    }
    public class CH_Psychometric_Model
    {
        public int ID { get; set; }
        public string CaseHistoryId { get; set; }
        public string CaseId { get; set; }
        public Nullable<int> RIASEC_Guided_Id { get; set; }
        public string PsychometricTestId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
    public class CH_CourseD_Model
    {
        public int ID { get; set; }
        public int CourDId { get; set; }
        public string CaseHistoryId { get; set; }
        public string CaseId { get; set; }
        public Nullable<int> CourseD_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
    public class CH_SkillT_Model
    {
        public int ID { get; set; }
        public string CaseHistoryId { get; set; }
        public string CaseId { get; set; }
        public Nullable<int> SkillT_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
    public class CH_Scheme_Model
    {
        public int ID { get; set; }
        public string CaseHistoryId { get; set; }
        public string CaseId { get; set; }
        public Nullable<int> Scheme_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
    public class CH_Scholarship_Model
    {
        public int ID { get; set; }
        public string CaseHistoryId { get; set; }
        public string CaseId { get; set; }
        public Nullable<int> Scholarship_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
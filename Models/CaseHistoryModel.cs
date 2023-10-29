using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class CaseHistoryModel
    {
        public Nullable<System.Guid> GudID { get; set; }
        public int Id { get; set; }
        public string CountdownStart { get; set; }
        public string CountdownEnd { get; set; }
        public Nullable<System.DateTime> DOC { get; set; }
        public string CaseID { get; set; }
        public Nullable<int> ClassId { get; set; }
        public string TypeCounsellor { get; set; }
        public string TypeQuery { get; set; }
        public string KeyWords { get; set; }
        public string Study10th { get; set; }
        public string Study12th { get; set; }
        public string Subject { get; set; }
        public string Counselling { get; set; }
        public string IsPsychometric { get; set; }
        public string Psychometric { get; set; }
        public string Recommendation { get; set; }
        public string AreasImprovement { get; set; }
        public string IsFollow { get; set; }
        public string FM { get; set; }
        public string FY { get; set; }
        public string IsGoalClear { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        //public List<CH_Psychometric_Model> CH_PHY_model { get; set; }
        //public List<CH_CourseD_Model> CH_CD_model {  get; set; }   
        //public List<CH_SkillT_Model> CH_SkillT_model {  get; set; }   
        //public List<CH_Scholarship_Model> CH_SSP_model {  get; set; }   
        //public List<CH_Scheme_Model> CH_SM_model {  get; set; }   
    }
    //public class CH_Psychometric_Model
    //{
    //    public int ID { get; set; }
    //    public string CaseHistoryId { get; set; }
    //    public string CaseId { get; set; }
    //    public Nullable<int> RIASEC_Guided_Id { get; set; }
    //    public string PsychometricTestId { get; set; }
    //    public string CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //}
    //public class CH_CourseD_Model
    //{
    //    public int ID { get; set; }
    //    public int CourDId { get; set; }
    //    public string CaseHistoryId { get; set; }
    //    public string CaseId { get; set; }
    //    public Nullable<int> CourseD_Id { get; set; }
    //    public string CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //}
    //public class CH_SkillT_Model
    //{
    //    public int ID { get; set; }
    //    public string CaseHistoryId { get; set; }
    //    public string CaseId { get; set; }
    //    public Nullable<int> SkillT_Id { get; set; }
    //    public string CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //}
    //public class CH_Scheme_Model
    //{
    //    public int ID { get; set; }
    //    public string CaseHistoryId { get; set; }
    //    public string CaseId { get; set; }
    //    public Nullable<int> CourseD_Id { get; set; }
    //    public string CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //}
    //public class CH_Scholarship_Model
    //{
    //    public int ID { get; set; }
    //    public string CaseHistoryId { get; set; }
    //    public string CaseId { get; set; }
    //    public Nullable<int> Scholarship_Id { get; set; }
    //    public string CreatedBy { get; set; }
    //    public Nullable<System.DateTime> CreatedOn { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public Nullable<System.DateTime> UpdatedOn { get; set; }
    //}

}
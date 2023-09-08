using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class CoursesDModel
    {
        public CoursesDModel() {
            ID = 0;
        }
        public int ID { get; set; }
        [Display (Name= "Name of the Course")]
        public string NameCourseEng { get; set; }
        [Display(Name = "पाठयक्रम का नाम")]
        public string NameCourseHindi { get; set; }
        [Display(Name = "Course Type")]
        public string CourseTypeEng { get; set; }
        [Display(Name = "कार्स का प्रकार")]
        public string CourseTypeHindi { get; set; }
        [Display(Name = "Job Opportunity")]
        public string JobOpportunityEng { get; set; }
        [Display(Name = "नौकरी का अवसर")]
        public string JobOpportunityHindi { get; set; }
        [Display(Name = "Course Duration")]
        public string CourseDurationEng { get; set; }
        [Display(Name = "पाठयक्रम की अवधि")]
        public string CourseDurationHindi { get; set; }
        [Display(Name = "Short Description of Course")]
        public string ShortDescriptionCourseEng { get; set; }
        [Display(Name = "पाठयक्रम का संक्षिप्त विवरण")]
        public string ShortDescriptionCourseHindi { get; set; }
        [Display(Name = "Eligibility")]
        public string EligibilityEng { get; set; }
        [Display(Name = "योग्यता")]
        public string EligibilityHindi { get; set; }
        [Display(Name = "Marks Criteria")]
        public string MarksCriteriaEng { get; set; }
        [Display(Name = "अंक मानदंड")]
        public string MarksCriteriaHindi { get; set; }
        [Display(Name = "Admission Process")]
        public string AdmissionProcessEng { get; set; }
        [Display(Name = "प्रवेश प्रक्रिया")]
        public string AdmissionProcessHindi { get; set; }
        [Display(Name = "Medium of Instructions")]
        public string MediumInstructionEng { get; set; }
        [Display(Name = "निर्देशों का माध्यम")]
        public string MediumInstructionHindi { get; set; }
        [Display(Name = "Hostel Availability")]
        public string HostelAvailabilityEng { get; set; }
        [Display(Name = "छात्रावास की उपलब्धता")]
        public string HostelAvailabilityHindi { get; set; }
        [Display(Name = "Available Scholarships")]
        public string AvailableScholarshipEng { get; set; }
        [Display(Name = "उपलब्ध छात्रवृत्ति")]
        public string AvailableScholarshipHindi { get; set; }
        [Display(Name = "Category")]
        public string CategoryEng { get; set; }
        [Display(Name = "श्रेणी")]
        public string CategoryHindi { get; set; }
        [Display(Name = "Category Other")]
        public string CategoryOtherEng { get; set; }
        [Display(Name = "श्रेणी अन्य")]
        public string CategoryOtherHindi { get; set; }
        [Display(Name = "College/University/Institute")]
        public string College_Unvty_InstEng { get; set; }
        [Display(Name = "कॉलेज/विश्वविद्यालय/संस्थान")]
        public string College_Unvty_InstHindi { get; set; }
        [Display(Name = "Fee Structure")]
        public string FeeStructureEng { get; set; }
        [Display(Name = "शुल्क संरचना")]
        public string FeeStructureHindi { get; set; }
        [Display(Name = "Status of Institution")]
        public string StatusInstitutionEng { get; set; }
        [Display(Name = "संस्था की स्थिति")]
        public string StatusInstitutionHindi { get; set; }
        [Display(Name = "District")]
        public string DistrictEng { get; set; }
        [Display(Name = "जिला")]
        public string DistrictHindi { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDt { get; set; }
    }
}
using Foolproof;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Web;
using System.Xml.Linq;

namespace UmangMicro.Models
{
    public class RegModel
    {
        public RegModel()
        {
            ID = 0;
        }
        public RegModel(string name) { }
        public int ID { get; set; }
        public string UseraspID { get; set; }
        [Required]
        [Display(Name="Registration Date")]
        public Nullable<System.DateTime> RegDate { get; set; }
         [Required]
        //[StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        //[StringLength(100, MinimumLength = 3)]
         [Required]
        public string FatherName { get; set; }
        //[StringLength(100, MinimumLength = 3)]
         [Required]
        public string MotherName { get; set; }
       // [Required]
        public Nullable<int> StateId { get; set; }
        [Required]
        public Nullable<int> DistrictId { get; set; }
        [Required]
        public Nullable<int> BlockId { get; set; }
        [Required]
        public Nullable<int> ClusterId { get; set; }
         [Required]
        //[StringLength(100, MinimumLength = 3)]
        public string Village { get; set; }
        //[Required]
        public string CaseID { get; set; }
        [Required]
        public string Visited { get; set; }
        [Required]
        public Nullable<System.DateTime> DOB { get; set; }
       [Required]
        public string Age { get; set; }
         [Required]
        //[Range(Int32.MinValue, 10)]
        //[Range(3000, 10000000, ErrorMessage = "Salary must be between 3000 and 10000000")]
        public string MobileNo { get; set; }
         [Required]
        public int IsSkillTraining { get; set; }
        [Required]
        public int IsMarriage { get; set; }
        [Required]
        public int IsStudy { get; set; }
        [Required]
        public string SocialClass { get; set; }
        [Required]
        public string TillStudied { get; set; }
        [Required]
        public int IsWork { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public int IsPsychometric { get; set; }
        //[RequiredIf("IsPsychometric", true, ErrorMessageResourceName = "PsyYes_Result")]
        public string PsyYes_Result { get; set; }
        [Required]
        public string Advice { get; set; }
        [Required]
        public Nullable<int> IsFollowUp { get; set; }
        public bool IsFollowUpCheck { get; set; }
        //[RequiredIf("IsFollowUpCheck", true, ErrorMessageResourceName = "FollowUp")]
        public string FollowUp { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDt { get; set; }
        public string UpdatedBY { get; set; }
        public Nullable<System.DateTime> UpdatedDt { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDt { get; set; }
        public int? LanguangeType { get; set; }
        public RegisterViewModel RegisterViModel { get; set; }    
        public String DisplayName1
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Name";
                }
                else
                {
                    CN = "नाम";
                }
                return CN;
            }
        }
        public String DisplayFN
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Father Name";
                }
                else
                {
                    CN = "पिता का नाम";
                }
                return CN;
            }
        }
        public String DisplayMN
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Mother Name";
                }
                else
                {
                    CN = "माँ का नाम";
                }
                return CN;
            }
        }
        public String DisplaySId
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "State";
                }
                else
                {
                    CN = "राज्य";
                }
                return CN;
            }
        }
        public String DisplayDId
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "District";
                }
                else
                {
                    CN = "जिला का नाम";
                }
                return CN;
            }
        }
        public String DisplayBId
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Block";
                }
                else
                {
                    CN = "ब्लाक का नाम";
                }
                return CN;
            }
        }
        public String DisplayClId
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Cluster";
                }
                else
                {
                    CN = "समूह का नाम";
                }
                return CN;
            }
        }
        public String DisplayVillageTxt
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Village";
                }
                else
                {
                    CN = "गाँव का नाम";
                }
                return CN;
            }
        }
        public String DisplayVisitedTxt
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Who has visited?";
                }
                else
                {
                    CN = "किसके द्वारा भ्रमण किया गया है?";
                }
                return CN;
            }
        }
        public String DisplayMobileTxt
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Mobile No";
                }
                else
                {
                    CN = "मोबाइल नंबर";
                }
                return CN;
            }
        }
        public String DisplayDOBTxt
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Date Of Birth";
                }
                else
                {
                    CN = "जन्म तिथि";
                }
                return CN;
            }
        }
        public String DisplayAgeTxt
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Age";
                }
                else
                {
                    CN = "उम्र";
                }
                return CN;
            }
        }
        public String DisplayIsSKTN
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Any skill training?";
                }
                else
                {
                    CN = "कोई कौशल प्रशिक्षण ?";
                }
                return CN;
            }
        }
        public String DisplayIsMG
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Is his marriage fixed?";
                }
                else
                {
                    CN = "क्या उसकी शादी तय हो गई है ?";
                }
                return CN;
            }
        }
        public String DisplayIsSTDY
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Is he/she reading?";
                }
                else
                {
                    CN = "क्या वो पढ़ रही /रहा है ?";
                }
                return CN;
            }
        }
        public String DisplaySC
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Social Class";
                }
                else
                {
                    CN = "सामजिक श्रेणी";
                }
                return CN;
            }
        }
        public String DisplayTillSTDY
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Where have you studied (Last class till which you studied)";
                }
                else
                {
                    CN = "कहाँ तक पढाई की है ( अंतिम कक्षा जहाँ तक पढाई की है )";
                }
                return CN;
            }
        }
        public String DisplayIsWK
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Is he/she working?";
                }
                else
                {
                    CN = "क्या वो काम कर रही/रहा है ?";
                }
                return CN;
            }
        }
        public String DisplayRESON
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "What is the reason for coming";
                }
                else
                {
                    CN = "आने का क्या कारण है :";
                }
                return CN;
            }
        }
        public String DisplayIsPSYMT
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Was psychometric test taken? If Yes";
                }
                else
                {
                    CN = "क्या सायकोमेट्रिक टेस्ट लिया गया ? अगर हाँ";
                }
                return CN;
            }
        }
        public String DisplayPSYRESULT
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "If yes, what was the result?";
                }
                else
                {
                    CN = "अगर हाँ ,तो क्या परिणाम निकला ?";
                }
                return CN;
            }
        }
        public String DisplayADVC
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "What advice was given :";
                }
                else
                {
                    CN = "क्या परामर्श दिया गया :";
                }
                return CN;
            }
        }
        public String DisplayIsFLWUP
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Follow-up";
                }
                else
                {
                    CN = "Follow-up";
                }
                return CN;
            }
        }
        public String DisplayFLWUP
        {
            get
            {
                String CN = String.Empty;
                if (LanguangeType == 1)
                {
                    CN = "Follow-up";
                }
                else
                {
                    CN = "फॉलो -उप";
                }
                return CN;
            }
        }
    }
    public class RegEnglabel
    {
        public const string Name = "Name";
        public readonly string FatherName = "Father Name";
    }

    
}
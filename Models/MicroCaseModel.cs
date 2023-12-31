﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UmangMicro.Models
{
    public class MicroCaseModel
    {
        public MicroCaseModel()
        {
            ID = 0;
        }
        public int ID { get; set; }
        [Display(Name = "Date")]
        public Nullable<DateTime> Date { get; set; }
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Display(Name = "Case Studies Description")]
        [AllowHtml]
        public string HtmlEditor { get; set; }
        public string PhotoPath { get; set; }
        [Display(Name = "Upload Image")]
        public HttpPostedFile Photo { get; set; }
        [Display(Name = "Upload Banner")]
        public HttpPostedFile Banner { get; set; }
        public string BannerPath { get; set; }
        [Display(Name = "Document")]
        public string Document { get; set; }
        public string DocumentPath { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }

    public class CaseDetView
    {
        public int ID { get; set; }
        public string MY { get; set; }
        public string Subject { get; set; }
        [AllowHtml]
        public string Desp { get; set; }
        public string Photo { get; set; }
        public string Banner { get; set; }
    }
}
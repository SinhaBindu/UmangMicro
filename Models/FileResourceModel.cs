using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class FileResourceModel
    {
        public int FileId_pk { get; set; }
        public string FileGuid { get; set; }
        [Required]
        [DisplayName("Document Type")]
        public string DocumentType { get; set; }
        public string LetterNo { get; set; }
        public string FileName { get; set; }
        [Required]
        [DisplayName("File Upload")]
        public HttpPostedFileBase file { get; set; }
        public string FileImage { get; set; }
        [Required]
        [DisplayName("Image Upload")]
        public HttpPostedFileBase Image { get; set; }
        public string AttachmentFile { get; set; }
        public string AttachmentImage { get; set; }
        [Required]
        [DisplayName("Date of Issue")]
        public Nullable<System.DateTime> DateofIssue { get; set; }
        [Required]
        [DisplayName("Title")]
        public string Subject { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Upload_date { get; set; }
        public string UplaodBy { get; set; }
    }
    public class resourcefilter
    {
        [DisplayName("Letter No")]
        public string LetterNo { get; set; }
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [DisplayName("Document Type")]
        public string doctype { get; set; }
        [DisplayName("Issue Date")]
        public DateTime? IssueDate { get; set; }
    }
}
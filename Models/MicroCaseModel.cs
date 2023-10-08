using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class MicroCaseModel
    {
        public MicroCaseModel()
        {
            ID = 0;
        }
        public int ID { get; set; }
        public string Subject { get; set; }
        public string HtmlEditor { get; set; }
        public string PhotoPath { get; set; }
        public HttpPostedFile Photo { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
    public class FilterModel
    {
    }
    public class FilterCanReg
    {
        public string FD { get; set; }
        public string TD { get; set; }
        [Display(Name ="District")]
        public string DistrictId { get; set; }
        [Display(Name = "Block")]
        public string BlockId { get; set; }
        [Display(Name = "School")]
        public string SchoolId { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UmangMicro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Trainingteacher
    {
        public int Id { get; set; }
        public Nullable<int> TrainingtypeId_fk { get; set; }
        public Nullable<System.Guid> NameofTeacher { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> IsDeletedOn { get; set; }
    }
}
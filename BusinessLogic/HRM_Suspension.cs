//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    
    public partial class HRM_Suspension
    {
        public decimal SuspensionCode { get; set; }
        public string SuspensionId { get; set; }
        public string EmployeeID { get; set; }
        public System.DateTime SuspensionDate { get; set; }
        public string Reason { get; set; }
        public System.DateTime DurationFrom { get; set; }
        public string IsContinuing { get; set; }
        public System.DateTime To { get; set; }
        public string RecommendedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Verdict { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string CompanyCode { get; set; }
    }
}
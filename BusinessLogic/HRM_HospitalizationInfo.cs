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
    
    public partial class HRM_HospitalizationInfo
    {
        public decimal autoId { get; set; }
        public string HospitalizationID { get; set; }
        public string EmployeeID { get; set; }
        public Nullable<System.DateTime> Issue_Date { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Remarks { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string CompanyCode { get; set; }
        public string UserInfoEmployeeID { get; set; }
    }
}

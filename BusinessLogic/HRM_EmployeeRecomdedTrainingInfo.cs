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
    
    public partial class HRM_EmployeeRecomdedTrainingInfo
    {
        public decimal autoId { get; set; }
        public string TrainingID { get; set; }
        public string EmployeeID { get; set; }
        public string TrainingTitleID { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> TrainingFrom { get; set; }
        public Nullable<System.DateTime> TrainingTo { get; set; }
        public string RecommendedBy { get; set; }
        public string ApprovalBy { get; set; }
        public string Remarks { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string UserInfoEmployeeID { get; set; }
        public string CompanyCode { get; set; }
    }
}
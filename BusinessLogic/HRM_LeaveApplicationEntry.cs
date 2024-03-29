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
    
    public partial class HRM_LeaveApplicationEntry
    {
        public decimal LeaveAppEntryCode { get; set; }
        public string LeaveAppEntryId { get; set; }
        public string EmployeeID { get; set; }
        public string LeaveTypeId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public decimal NoOfDay { get; set; }
        public string HalfDay { get; set; }
        public string FirstOrSecondHalf { get; set; }
        public string Reason { get; set; }
        public string BossEmpAutoId { get; set; }
        public string HOD { get; set; }
        public string IsApproved { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ConfirmationRemarks { get; set; }
        public string CompanyCode { get; set; }
        public string HODApprovalStatus { get; set; }
        public string HODApprovalRemarks { get; set; }
        public string HRApprovalStatus { get; set; }
        public string HRApprovalRemarks { get; set; }
        public string ApplyLeaveFormat { get; set; }
        public Nullable<System.DateTime> ShortLeaveFrom { get; set; }
        public Nullable<System.DateTime> ShortLeaveTo { get; set; }
        public Nullable<System.TimeSpan> ShortLeaveTime { get; set; }
        public string LeaveApplyProcess { get; set; }
    }
}

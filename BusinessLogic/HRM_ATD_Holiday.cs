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
    
    public partial class HRM_ATD_Holiday
    {
        public decimal autoId { get; set; }
        public string HolidayCode { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public byte NoOfDays { get; set; }
        public string HolidayName { get; set; }
        public string HolidayType { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
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
    
    public partial class HRM_Def_IdSetup
    {
        public decimal autoId { get; set; }
        public string IdSetupCode { get; set; }
        public string SetupNameCode { get; set; }
        public string Description { get; set; }
        public string numberingMethod { get; set; }
        public decimal Length { get; set; }
        public decimal startingNumber { get; set; }
        public decimal Increment { get; set; }
        public decimal resetDuration { get; set; }
        public string resetDurationType_Code { get; set; }
        public string prefix { get; set; }
        public string suffix { get; set; }
        public string ZeroPadding { get; set; }
        public string LUser { get; set; }
        public Nullable<System.DateTime> LDate { get; set; }
        public string LIP { get; set; }
        public string LMAC { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
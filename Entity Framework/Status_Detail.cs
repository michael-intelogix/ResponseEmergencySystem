//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ResponseEmergencySystem.Entity_Framework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Status_Detail
    {
        public System.Guid ID_StatusDetail { get; set; }
        public string name { get; set; }
    
        public virtual Incident_Report Incident_Report { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ResponseEmergencySystem.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Incidents
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Incidents()
        {
            this.Captures = new HashSet<Captures>();
            this.InjuredPersons = new HashSet<InjuredPersons>();
            this.Locations = new HashSet<Locations>();
        }
    
        public System.Guid ID_Incident { get; set; }
        public string Folio { get; set; }
        public Nullable<System.DateTime> IncidentDate { get; set; }
        public Nullable<System.DateTime> IncidentCloseDate { get; set; }
        public bool PoliceReport { get; set; }
        public string CitationReportNumber { get; set; }
        public string ManifestNumber { get; set; }
        public string LocationReferences { get; set; }
        public string IncidentLatitude { get; set; }
        public string IncidentLongitude { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string ID_Truck { get; set; }
        public Nullable<bool> TruckDamage { get; set; }
        public bool TruckCanMove { get; set; }
        public bool TruckNeedCrane { get; set; }
        public string ID_Trailer { get; set; }
        public Nullable<bool> TrailerDamage { get; set; }
        public bool TrailerCanMove { get; set; }
        public bool TrailerNeedCrane { get; set; }
        public Nullable<bool> CargoSpill { get; set; }
        public bool Status { get; set; }
        public string ID_Driver { get; set; }
        public string ID_City { get; set; }
        public string ID_State { get; set; }
        public string ID_Broker { get; set; }
        public string ID_StatusDetail { get; set; }
        public string TruckNumber { get; set; }
        public string TrailerNumber { get; set; }
        public string TrailerCommodity { get; set; }
        public string DriverName { get; set; }
        public Nullable<bool> DSamsara { get; set; }
        public Nullable<bool> TSamsara { get; set; }
        public string ID_Broker2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Captures> Captures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InjuredPersons> InjuredPersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locations> Locations { get; set; }
    }
}

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
    
    public partial class Location
    {
        public System.Guid ID_Location { get; set; }
        public System.Guid ID_City { get; set; }
        public System.Guid ID_State { get; set; }
        public string Highway { get; set; }
        public string References { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

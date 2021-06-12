using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Incident
    {
        public Guid ID_Incident { get; set; } 
        public string  Folio { get; set; } 
        public DateTime IncidentDate { get; set; } 
        public DateTime? IncidentCloseDate { get; set; } 
        public bool PoliceReport { get; set; } 
        public string CitationReportNumber { get; set; } 
        public string ManifestNumber { get; set; } 
        public string LocationReferences { get; set; } 
        public string IncidentLatitude { get; set; } 
        public string IncidentLongitude { get; set; } 
        public string Comments { get; set; } 
        public Truck truck { get; set; } 
        public bool TruckDamage { get; set; } 
        public bool TruckCanMove { get; set; } 
        public bool TruckNeedCrane { get; set; }
        public Trailer trailer { get; }
        public bool TrailerDamage { get; set; } 
        public bool TrailerCanMove { get; set; }
        public bool TrailerNeedCrane { get; set; }
        public Driver driver;
        public string ID_City { get; set; }
        public string ID_State { get; set; }
        public string ID_Broker { get; set; }
        public Broker broker { get; set; }
        public Broker TrailerBroker { get; set; }
        public string ID_StatusDetail { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; } 

        public Incident(
            Guid id, 
            string Folio,
            DateTime IncidentDate,
            DateTime IncidentCloseDate,
            bool PoliceReport, 
            string CitationReportNumber, 
            string ManifestNumber, 
            string LocationReferences, 
            string IncidentLatitude, 
            string IncidentLongitude,
            string Comments,
            Truck truck,
            bool TruckDamage,
            bool TruckCanMove,
            bool TruckNeedCrane,
            Trailer trailer,
            bool TrailerDamage,
            bool TrailerCanMove,
            bool TrailerNeedCrane,
            Driver driver,
            string ID_City,
            string ID_State,
            string ID_Broker,
            Broker broker,
            string ID_StatusDetail,
            string Description,
            string Name,
            string PhoneNumber
        )
        {
            ID_Incident = id;
            this.Folio = Folio;
            this.IncidentDate = IncidentDate;
            this.IncidentCloseDate = IncidentCloseDate;
            this.PoliceReport = PoliceReport;
            this.CitationReportNumber = CitationReportNumber;
            this.ManifestNumber = ManifestNumber;
            this.LocationReferences = LocationReferences;
            this.IncidentLatitude = IncidentLatitude;
            this.IncidentLongitude = IncidentLongitude;
            this.Comments = Comments;
            this.truck = truck;
            this.TruckDamage = TruckDamage;
            this.TruckCanMove = TruckCanMove;
            this.TruckNeedCrane = TruckNeedCrane;
            this.trailer = trailer;
            this.TrailerDamage = TrailerDamage;
            this.TrailerCanMove = TrailerCanMove;
            this.TrailerNeedCrane = TrailerNeedCrane;
            this.driver = driver;
            this.ID_City = ID_City;
            this.ID_State = ID_State;
            this.ID_Broker = ID_Broker;
            this.broker = broker;
            this.ID_StatusDetail = ID_StatusDetail;
            this.Description = Description;
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
        }


        public Incident(
           Guid id,
           string Folio,
           DateTime IncidentDate,
           DateTime IncidentCloseDate,
           bool PoliceReport,
           string CitationReportNumber,
           string ManifestNumber,
           string LocationReferences,
           string IncidentLatitude,
           string IncidentLongitude,
           string Comments,
           Truck truck,
           bool TruckDamage,
           bool TruckCanMove,
           bool TruckNeedCrane,
           Trailer trailer,
           bool TrailerDamage,
           bool TrailerCanMove,
           bool TrailerNeedCrane,
           Driver driver,
           string ID_City,
           string ID_State,
           string ID_Broker,
           Broker broker,
           Broker trailerBroker,
           string ID_StatusDetail,
           string Description,
           string Name,
           string PhoneNumber
       )
        {
            ID_Incident = id;
            this.Folio = Folio;
            this.IncidentDate = IncidentDate;
            this.IncidentCloseDate = IncidentCloseDate;
            this.PoliceReport = PoliceReport;
            this.CitationReportNumber = CitationReportNumber;
            this.ManifestNumber = ManifestNumber;
            this.LocationReferences = LocationReferences;
            this.IncidentLatitude = IncidentLatitude;
            this.IncidentLongitude = IncidentLongitude;
            this.Comments = Comments;
            this.truck = truck;
            this.TruckDamage = TruckDamage;
            this.TruckCanMove = TruckCanMove;
            this.TruckNeedCrane = TruckNeedCrane;
            this.trailer = trailer;
            this.TrailerDamage = TrailerDamage;
            this.TrailerCanMove = TrailerCanMove;
            this.TrailerNeedCrane = TrailerNeedCrane;
            this.driver = driver;
            this.ID_City = ID_City;
            this.ID_State = ID_State;
            this.ID_Broker = ID_Broker;
            this.broker = broker;
            this.TrailerBroker = trailerBroker;
            this.ID_StatusDetail = ID_StatusDetail;
            this.Description = Description;
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
        }
    }
}

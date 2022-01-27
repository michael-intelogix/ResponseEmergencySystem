using ResponseEmergencySystem.Builders;
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
        public string Folio { get; set; }
        public string ClaimNumber { get; set; }
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
        public Driver driver { get; }
        public Employee driver2 { get; set; }
        public string ID_City { get; set; }
        public string ID_State { get; set; }
        public string ID_Broker { get; set; }
        public Broker broker { get; set; }
        public Broker TrailerBroker { get; set; }
        public string ID_StatusDetail { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Vehicle truck1 { get; set; }
        public Vehicle trailer1 { get; set; }

        public List<Location> locations { get; set; }

        public string ID_Broker2 { get; }

        public bool isNew { get; }

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
            Employee driver,
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
            this.driver2 = driver;
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
           string PhoneNumber,
           bool isNew
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
            this.isNew = isNew;
        }

        public Incident(
            Guid id,
            string claimNumber,
            bool PoliceReport,
            string CitationReportNumber,
            string ManifestNumber,
            DateTime IncidentDate,
            Location location,
            string Comments,
            Vehicle truck,
            Vehicle trailer,
            Employee driver,
            string ID_City,
            string ID_State,
            string ID_Broker,
            string ID_Broker2
        )
        {
            ID_Incident = id;
            this.ClaimNumber = claimNumber;
            this.PoliceReport = PoliceReport;
            this.CitationReportNumber = CitationReportNumber;
            this.ManifestNumber = ManifestNumber;
            this.IncidentDate = IncidentDate;
            this.LocationReferences = location.Description;
            this.IncidentLatitude = location.Latitude;
            this.IncidentLongitude = location.Longitude;
            this.Comments = Comments;
            this.truck1 = truck;
            this.trailer1 = trailer;
            this.driver2 = driver;
            this.ID_City = ID_City;
            this.ID_State = ID_State;
            this.ID_Broker = ID_Broker;
            this.ID_Broker2 = ID_Broker2;
        }

        public Incident(
            Guid id,
            bool PoliceReport,
            string CitationReportNumber,
            string ManifestNumber,
            DateTime IncidentDate,
            Location location,
            string Comments,
            Truck truck,
            Trailer trailer,
            Driver driver,
            string ID_City,
            string ID_State,
            string ID_Broker,
            string ID_Broker2
        )
        {
            ID_Incident = id;
            this.PoliceReport = PoliceReport;
            this.CitationReportNumber = CitationReportNumber;
            this.ManifestNumber = ManifestNumber;
            this.IncidentDate = IncidentDate;
            this.LocationReferences = location.Description;
            this.IncidentLatitude = location.Latitude;
            this.IncidentLongitude = location.Longitude;
            this.Comments = Comments;
            this.truck = truck;
            this.trailer = trailer;
            this.driver = driver;
            this.ID_City = ID_City;
            this.ID_State = ID_State;
            this.ID_Broker = ID_Broker;
            this.ID_Broker2 = ID_Broker2;
        }

        public Incident(
            Guid id,
            string Folio,
            DateTime IncidentDate,
            bool PoliceReport,
            string CitationReportNumber,
            string ManifestNumber,
            Location location,
            string Comments,
            Truck truck,
            Trailer trailer,
            Driver driver,
            string ID_City,
            string ID_State,
            Broker broker,
            Broker trailerBroker,
            string statusDetail,
            string Description,
            string Name,
            string PhoneNumber,
            bool newDriver
        )
        {
            ID_Incident = id;
            this.Folio = Folio;
            this.IncidentDate = IncidentDate;
            this.PoliceReport = PoliceReport;
            this.CitationReportNumber = CitationReportNumber;
            this.ManifestNumber = ManifestNumber;
            this.LocationReferences = location.Description;
            this.IncidentLatitude = location.Latitude;
            this.IncidentLongitude = location.Longitude;
            this.Comments = Comments;
            this.truck = truck;
            this.trailer = trailer;
            this.driver = driver;
            this.ID_City = ID_City;
            this.ID_State = ID_State;
            this.ID_Broker = broker.ID_Broker;
            this.ID_Broker2 = trailerBroker.ID_Broker;
            this.broker = broker;
            this.TrailerBroker = trailerBroker;
            this.ID_StatusDetail = statusDetail;
            this.Description = Description;
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.isNew = newDriver;
        }
    }
}

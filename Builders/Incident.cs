using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Builders
{
    public sealed class Incident
    {
        public Guid ID_Incident { get; internal set; }
        public string ID_StatusDetail { get; set; }
        public string Folio { get; internal set; }
        public string ClaimNumber { get; internal set; }
        public DateTime IncidentDate { get; internal set; }
        public DateTime? IncidentCloseDate { get; internal set; }
        public bool PoliceReport { get; internal set; }
        public string CitationReportNumber { get; internal set; }
        public string ManifestNumber { get; internal set; }
        public Location Location { get; internal set; }
        public Vehicle Truck { get; internal set; } 
        public Vehicle Trailer { get; internal set; }
        public Employee Driver { get; internal set; }
        public string Comments { get; internal set; }

        public Incident ShallowCopy()
        {
            return (Incident)this.MemberwiseClone();
        }

    }

    public class Location
    {
        public string ID_State { get; set; }
        public string ID_City { get; set; }
        public string Latitude { get; internal set; }
        public string Longitude { get; internal set; }
        public string Description { get; internal set; }
        public DateTime CreatedAt { get; internal set; }

        public Location(string state, string city, string lat, string lon, string des)
        {
            ID_State = state;
            ID_City = city;
            Latitude = lat;
            Longitude = lon;
            Description = des;
        }
    }


    public abstract class FunctionalIncidentBuilder<TSubject, TSelf>
    where TSelf : FunctionalIncidentBuilder<TSubject, TSelf>
    where TSubject : new()
    {
        private readonly List<Func<Incident, Incident>> actions = new List<Func<Incident, Incident>>();

        public TSelf Do(Action<Incident> action) => AddAction(action);

        public Incident Build() => actions.Aggregate(new Incident(), (i, f) => f(i));

        private TSelf AddAction(Action<Incident> action)
        {
            actions.Add(i =>
            {
                action(i);
                return i;
            });

            return (TSelf)this;
        }
    }

    public sealed class IncidentBuilder : FunctionalIncidentBuilder<Incident, IncidentBuilder>
    {
        public IncidentBuilder SetID(Guid incidentId) => Do(i => i.ID_Incident = incidentId);

        public IncidentBuilder SetStatusDetail(string statusDetail) => Do(i => i.ID_StatusDetail = statusDetail);

        public IncidentBuilder SetFolio(string folio) => Do(i => i.Folio = folio);

        public IncidentBuilder SetClaimNumber(string claim) => Do(i => i.ClaimNumber = claim);

        public IncidentBuilder SetOpenDate(DateTime date) => Do(i => i.IncidentDate = date);

        public IncidentBuilder HasPoliceReport(bool policeReport, string citationReport = "") => Do(i => {
            i.PoliceReport = policeReport;
            i.CitationReportNumber = citationReport;
        });

        public IncidentBuilder SetCitationReport(string citationReport) => Do(i => i.CitationReportNumber = citationReport);

        public IncidentBuilder SetManifestNumber(string manifest) => Do(i => i.ManifestNumber = manifest);

        public IncidentBuilder SetLocation(Location location) => Do(i => {
            i.Location = location;
        });

        public IncidentBuilder SetTruck(Vehicle truck) => Do(i => i.Truck = truck);

        public IncidentBuilder SetTrailer(Vehicle trailer) => Do(i => i.Trailer = trailer);

        public IncidentBuilder SetDriver(Employee driver) => Do(i => i.Driver = driver);

        public IncidentBuilder SetComments(string comments) => Do(i => i.Comments = comments);
    }

}

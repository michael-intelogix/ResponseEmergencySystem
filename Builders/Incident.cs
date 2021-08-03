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
    }

    public class Location
    {
        public string ID_State { get; set; }
        public string ID_City { get; set; }
        public string Latitude { get; internal set; }
        public string Longitude { get; internal set; }
        public string Description { get; internal set; }
        public DateTime CreatedAt { get; internal set; }

        public Location(string lat, string lon, string des, DateTime createdAt)
        {
            Latitude = lat;
            Longitude = lon;
            Description = des;
            CreatedAt = createdAt;
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

    public sealed class IncidentBuilder : FunctionalEmployeeBuilder<Incident, IncidentBuilder>
    {

    }

}

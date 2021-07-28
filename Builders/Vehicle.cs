using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Builders
{
    public sealed class Vehicle
    {
        public Guid ID { get; set; }
        public Guid ID_Vehicle { get; set; }
        public Guid ID_General { get; set; }
        public string ID_Samsara { get; set; }
        public string Name { get; set; }
        public string VinNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public string Commodity { get; set; }
        public string Description { get; set; }
        public bool Exists { get; set; } = false;
        public bool IsLocal { get; set; } = false;
        public bool IsSamsara { get; set; } = false;
        public bool IsTruck { get; set; }
        public bool IsTrailer{ get; set; }
        public string Status { get; private set; }

        public Vehicle()
        {
            this.Status = "empty";
            if (IsSamsara && !Exists)
                SetNewVehicle();
        }

        public bool ValidateVinNumber(string newVinNumber)
        {
            string newValue = newVinNumber.Trim();
            if(this.VinNumber != newValue && newValue != "")
            {
                this.VinNumber = newValue ;
                this.Status = this.Exists ? "updated" : "added";
                return true;
            }

            return false;
        }


        public bool ValidateSerialNumber(string newSerialNumber)
        {
            string newValue = newSerialNumber.Trim();
            if (this.SerialNumber != newValue && newValue != "")
            {
                this.SerialNumber = newValue;
                this.Status = this.Exists ? "updated" : "added";
                return true;
            }

            return false;
        }

        public bool ValidateMake(string newMake)
        {
            string newValue = newMake.Trim();
            if (this.Make != newValue && newValue != "")
            {
                this.Make = newValue;
                this.Status = this.Exists ? "updated" : "added";
                return true;
            }

            return false;
        }

        public bool ValidateModel(string newModel)
        {
            string newValue = newModel.Trim();
            if (this.Model != newValue && newValue != "")
            {
                this.Model = newValue;
                this.Status = this.Exists ? "updated" : "added";
                return true;
            }

            return false;
        }

        public bool ValidateYear(string newYear)
        {
            string newValue = newYear.Trim();
            if (this.Year.ToString() != newValue && newValue != "")
            {
                int year = 0;
                if (!int.TryParse(newValue, out year))
                    return false;

                this.Year = year;
                this.Status = this.Exists ? "updated" : "added";
                return true;
            }

            return false;
        }

        public bool ValidateLicensePlate(string newLicensePlate)
        {
            string newValue = newLicensePlate.Trim();
            if (this.LicensePlate != newValue && newValue != "")
            {
                this.LicensePlate = newValue;
                this.Status = this.Exists ? "updated" : "added";
                return true;
            }

            return false;
        }

        public bool ValidateCommodity(string newCommodity)
        {
            string newValue = newCommodity.Trim();
            if (this.Commodity != newValue && newValue != "")
            {
                this.Commodity = newValue;
                this.Status = this.Exists ? "updated" : "added";
                return true;
            }

            return false;
        }

        public void SetNewVehicle ()
        {
            Status = "added";
        }
    }


    public  abstract class FunctionalVehicleBuilder<TSubject, TSelf>
        where TSelf : FunctionalVehicleBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Vehicle, Vehicle>> actions = new List<Func<Vehicle, Vehicle>>();

        protected TSelf Do(Action<Vehicle> action) => AddAction(action);

        public Vehicle Build() => actions.Aggregate(new Vehicle(), (e, f) => f(e));

        private TSelf AddAction(Action<Vehicle> action) 
        {
            actions.Add(e =>
            {
                action(e);
                return e;
            });

            return (TSelf)this;
        }
    }


    public sealed class VehicleBuilder : FunctionalVehicleBuilder<Vehicle, VehicleBuilder>
    {
        public VehicleBuilder SetName(string name) => Do(v => v.Name = name);
        public VehicleBuilder SetVinNumber(string vinNumber) => Do(v => v.VinNumber = vinNumber);
        public VehicleBuilder SetSerialNumber(string serialNumber) => Do(v => v.SerialNumber = serialNumber);
        public VehicleBuilder SetMake(string make) => Do(v => v.Make = make);
        public VehicleBuilder SetModel(string model) => Do(v => v.Model = model);
        public VehicleBuilder SetYear(string year) => Do(v => {
            int y = 0;
            if (Int32.TryParse(year, out y))
                v.Year = y;
        });
        public VehicleBuilder SetLicensePlate(string license) => Do(v => v.LicensePlate = license);

        public VehicleBuilder SetCommodity(string commodity) => Do(v => v.Commodity = commodity);

        public VehicleBuilder SetDescription(string description) => Do(v => v.Description = description);

        public VehicleBuilder IsRegisteredInGeneral(Guid generalID, string samsaraID) => Do(v => {
            v.ID_General = generalID;
            v.ID_Samsara = samsaraID;

            if (generalID == Guid.Empty)
                v.IsSamsara = true;
            else
                v.IsLocal = true;
        });
        public VehicleBuilder HasVehicleID(Guid vehicleID) => Do(v => {
            v.ID_Vehicle = vehicleID;
            if (vehicleID != Guid.Empty)
                v.Exists = true;
        });

        public VehicleBuilder VehicleType(string type) => Do(v => { 
            switch(type)
            {
                case "truck":
                    v.IsTruck = true;
                    break;
                case "trailer":
                    v.IsTrailer = true;
                    break;
            }
                
        });

        public VehicleBuilder NewVehicle() => Do(v => v.ID = Guid.NewGuid());
    }
    
}

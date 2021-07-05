﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models.Samsara
{
    public class TruckTrailer
    {
        public string ID_Samsara { get; set; }
        public string Name { get; set; }
        public string VinNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string LicensePlate { get; set; }

        public TruckTrailer(string name, string vin, string serial, string make, string model, string year, string plate, string ID)
        {
            Name = name;
            VinNumber = vin;
            SerialNumber = serial;
            Make = make;
            Model = model;
            Year = year;
            LicensePlate = plate;
            ID_Samsara = ID;
        }
    }
}

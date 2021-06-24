﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Reason
    {
        public string Name { get; }
        public string Description { get; }

        public Reason(string name)
        {
            Name = name;

        }
    }
}
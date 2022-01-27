using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Action
    {
        public string Name { get; }
        public string Description { get; }

        public Action(string name)
        {
            Name = name;

        }
    }
}

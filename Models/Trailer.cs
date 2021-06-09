using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Trailer
    {
        public Guid ID_Trailer { get; }
        public string TrailerNumber { get; }
        public string Commodity { get; }
        public bool CargoSpill { get; set; }

        public Trailer(Guid id, string number, string commodity, bool spill)
        {
            ID_Trailer = id;
            TrailerNumber = number;
            Commodity = commodity;
            CargoSpill = spill;
        }

    }
}

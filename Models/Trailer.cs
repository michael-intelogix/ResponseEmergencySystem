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

        public string TowingName { get; }
        public string TowedTo { get; }

        public bool Damages { get; }

        public bool CanMove { get; }

        public bool NeedCrane { get; }

        public Trailer(Guid id, string number, string commodity, bool spill)
        {
            ID_Trailer = id;
            TrailerNumber = number;
            Commodity = commodity;
            CargoSpill = spill;
        }

        public Trailer(Guid id, string number, string commodity, bool spill, string towingName, string towedTo)
        {
            ID_Trailer = id;
            TrailerNumber = number;
            Commodity = commodity;
            CargoSpill = spill;
            TowingName = towingName;
            TowedTo = towedTo; 
        }

        public Trailer(string id, string number, string commodity, bool spill, bool damages, bool canMove, bool needCrane)
        {
            ID_Trailer = Guid.Parse(id);
            TrailerNumber = number;
            Commodity = commodity;
            CargoSpill = spill;
            Damages = damages;
            CanMove = canMove;
            NeedCrane = needCrane;
        }

        public Trailer(Guid id, string number, string commodity, bool spill, string towingName, string towedTo, bool damages, bool canMove, bool needCrane)
        {
            ID_Trailer = id;
            TrailerNumber = number;
            Commodity = commodity;
            CargoSpill = spill;
            TowingName = towingName;
            TowedTo = towedTo;
            Damages = damages;
            CanMove = canMove;
            NeedCrane = needCrane;
        }
    }
}

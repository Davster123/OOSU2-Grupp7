using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Resurs
    {
        public int ResursID { get; set; }
        public string Namn { get; set; }
        public string Typ { get; set; }
        public int Kapacitet { get; set; }
        public int Utrustning { get; set; }
        public string Status { get; set; }
        public string Beskrivning { get; set; }

        // Relation: En resurs kan ha flera utrustningsartiklar och många bokningar över tid.
        public ICollection<Utrustning> Utrustningar { get; set; }
        public ICollection<Bokning> Bokningar { get; set; }

    }
}

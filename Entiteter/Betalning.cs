using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Betalning
    {
        public int BetalningID { get; set; }
        public int MedlemID { get; set; }
        public DateTime Köpdatum { get; set; }
        public decimal Belopp { get; set; }
        public DateTime Forfallodatum { get; set; }
        public DateTime BataldDatum { get; set; } //datumet då betalningen faktiskt genomfördes
        public string Status { get; set; } //aktuella status för betalning
    }
}

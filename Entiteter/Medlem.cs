using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Medlem
    {
        public int MedlemID { get; set; }
        public string Namn { get; set; }
        public string Telefonnummer { get; set; }
        public string Email { get; set; }
        public string MedlemskapsNivå { get; set; }
        public string Betalstatus { get; set; }

        public int Poäng { get; set; }

        public byte[]? Profilbild { get; set; }

        public string? Beskrivning { get; set; }
        public string Losenord { get; set; }

        public int AntalBokningar { get; set; }

        // Navigeringsegenskaper som gör det möjligt för Entity Framework att automatiskt hämta alla bokningar och betalningar som tillhör medlemmen.
        public ICollection<Bokning> Bokningar { get; set; }
        public ICollection<Betalning> Betalningar { get; set; }

    }
}

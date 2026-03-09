using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Bokning
    {
        public int BokningID { get; set; }
        public DateTime Datum { get; set; }
        public int MedlemID { get; set; }
        public virtual Medlem Medlem { get; set; }
        public int ResursID { get; set; }
        public virtual Resurs Resurs { get; set; }
        public int? UtrustningID { get; set; } // frågetecknet gör att fältet kan vara null
        public virtual Utrustning? Utrustning { get; set; }
        public string? Deltagare { get; set; }

        public TimeSpan Starttid { get; set; }
        public TimeSpan Sluttid { get; set; }
    }
}

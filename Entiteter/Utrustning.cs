using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteter
{
    public class Utrustning
    {
        public int UtrustningID { get; set; }
        public string ArtikelNamn { get; set; }
        public string Kategori { get; set; }
        public string Skick { get; set; }
        public int ResursID { get; set; }
        public Resurs Resurs { get; set; }
    }
}

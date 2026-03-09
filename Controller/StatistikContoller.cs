using Datalager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class StatistikController
    {
        private readonly UnitOfWork _uow;

        public StatistikController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public int TotaltAntalMedlemmar()
        {
            return _uow.MedlemRepository.Count(); //Räknar hur många medlemmar somn existerar i databasen
        }

        public int AntalAktivaBokningar()
        {
            return _uow.BokningRepository.Count(); //Räknar antalet bokningar som finns
        }

        public decimal TotalIntäkt()
        {
            return _uow.BetalningRepository.Find(b => b.Status == "Betald") //Systemet tar endast med betalningar som är betalda
                .Sum(b => b.Belopp); //Summerar de totala intäkterna
        }

    }
}

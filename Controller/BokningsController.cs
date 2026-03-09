using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class BokningsController
    {
        private readonly UnitOfWork _uow; //Används för att komma åt databasen via repositories

        public BokningsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Bokning> HämtaAllaBokningar()
        {
            return _uow.BokningRepository.GetAll().ToList();
        }

        public bool SkapaBokning(int medlemId, int resursId, int? utrustningId, string deltagare, DateTime datum, TimeSpan start, TimeSpan slut, out string fel)
        {
            fel = "";

            //Starttid måste vara före sluttid
            if (start >= slut)
            {
                fel = "Starttid måste vara före sluttid.";
                return false;
            }

            // Kontrollera om resursen är upptagen (kollar efter överlapp)
            var krock = _uow.BokningRepository.Find(b =>
                b.ResursID == resursId &&
                b.Datum.Date == datum.Date &&
                ((start >= b.Starttid && start < b.Sluttid) || (slut > b.Starttid && slut <= b.Sluttid))).Any();

            if (krock)
            {
                fel = "Resursen är redan bokad under denna tid.";
                return false;
            }

            var nyBokning = new Bokning
            {
                MedlemID = medlemId,
                ResursID = resursId,
                UtrustningID = utrustningId,
                Deltagare = deltagare,
                Datum = datum,
                Starttid = start,
                Sluttid = slut
            };

            _uow.BokningRepository.Add(nyBokning);
            _uow.Save(); // Sparar bokningen i databasen
            return true;
        }

        public void UppdateraBokning(Bokning bokning)
        {
            if (bokning != null)
            {
                _uow.BokningRepository.Update(bokning);
                _uow.Save(); //Sparar ändringar i databasen
            }
        }

        public void TaBortBokning(int id)
        {
            var bokning = _uow.BokningRepository.FirstOrDefault(x => x.BokningID == id);
            if (bokning != null)
            {
                _uow.BokningRepository.Remove(bokning);
                _uow.Save(); //Tar bort bokning från databasen
            }
        }


    }
}

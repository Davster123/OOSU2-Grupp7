using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ResursController
    {
        private readonly UnitOfWork _uow;

        public ResursController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Resurs> HämtaAllaResurser()
        {
            return _uow.ResursRepository.GetAll().ToList();
        }

        public List<Resurs> HämtaTillgängligaResurser()
        {
            return _uow.ResursRepository.Find(r => r.Status == "Tillgänglig").ToList(); // Visar endast resurser som är markerade som tillgängliga
        }

        public void UppdateraResursStatus(int resursId, string nyStatus)
        {
            var resurs = _uow.ResursRepository.FirstOrDefault(r => r.ResursID == resursId); //Hämtar rätt resurs från databasen
            if (resurs != null) //Dubbelkollar så resursen finns
            {
                resurs.Status = nyStatus; //Ändrar status, ex tillgänglig eller upptagen
                _uow.ResursRepository.Update(resurs);
                _uow.Save();
            }
        }

    }
}

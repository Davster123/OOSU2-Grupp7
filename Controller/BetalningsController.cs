using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalager;
using Entiteter;

namespace Controller
{
    public class BetalningsController
    {
        private readonly UnitOfWork _uow; // Gör så att controllern kan jobba mot databasen

        public BetalningsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Betalning> HämtaBetalningarFörMedlem(int medlemId)
        {
            return _uow.BetalningRepository.Find(b => b.MedlemID == medlemId).ToList(); //Systemet hämtar betalningar som tillhör rätt medlem                                                                                      
        }

        public void RegistreraBetalning(int betalningId)
        {
            var betalning = _uow.BetalningRepository.FirstOrDefault(b => b.BetalningID == betalningId); //Systemet letar upp rätt betalning
            if (betalning != null) //Kollar om betalningen finns
            {
                betalning.Status = "Betald";
                betalning.BataldDatum = DateTime.Now;
                _uow.BetalningRepository.Update(betalning);

                var medlem = _uow.MedlemRepository.FirstOrDefault(m => m.MedlemID == betalning.MedlemID); //Kopplar rätt medlem till rätt betalning
                if (medlem != null)
                {
                    medlem.Betalstatus = "Betald";
                    _uow.MedlemRepository.Update(medlem);
                }

                _uow.Save();
            }
        }

    }
}

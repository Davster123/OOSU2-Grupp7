using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Controller
{
    public class MedlemsController
    {
        private readonly UnitOfWork _uow;

        public MedlemsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Medlem> HämtaAllaMedlemmar() //Visar alla medlemmar
        {
            return _uow.MedlemRepository.GetAll().ToList();
        }

        public void UppdateraMedlem(Medlem medlem)
        {
            _uow.MedlemRepository.Update(medlem);
            _uow.Save(); //Sparar alla uppdateringar om medlemmar i databasen
        }
        public void TaBortMedlem(Medlem medlem)
        {
            if (medlem != null)
            {
                _uow.MedlemRepository.Remove(medlem);
                _uow.Save(); //När medlem tagits bort sparas det i databasen
            }
        }

        public bool LäggTillMedlem(string namn, string telefonnummer, string email, string medlemskapsnivå, string betalningsstatus, string losenord, out string fel)
        {
            fel = "";

            if (string.IsNullOrWhiteSpace(namn))
            {
                fel = "Namn måste fyllas i!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(medlemskapsnivå) || medlemskapsnivå == "Välj nivå")
            {
                fel = "Välj medlemskapsnivå.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(betalningsstatus) || betalningsstatus == "Välj status")
            {
                fel = "Välj betalningsstatus.";
                return false;
            }

            string emailTrim = (email ?? "").Trim();
            if (emailTrim.Length > 0 && !emailTrim.Contains("@"))
            {
                fel = "Email måste innehålla @.";
                return false;
            }

            if (emailTrim.Length > 0 && _uow.MedlemRepository.FirstOrDefault(m => m.Email == emailTrim) != null)
            {
                fel = "Email finns redan registrerad";
                return false;
            }

            if (string.IsNullOrWhiteSpace(losenord) || losenord.Length < 4)
            {
                fel = "Lösenordet måste vara minst 4 tecken långt.";
                return false;
            }

            var medlem = new Medlem
            {
                Namn = namn.Trim(),
                Telefonnummer = (telefonnummer ?? "").Trim(),
                Email = emailTrim,
                MedlemskapsNivå = medlemskapsnivå,
                Betalstatus = betalningsstatus,
                Losenord = losenord, 
                Poäng = 0            
            };

            _uow.MedlemRepository.Add(medlem);
            _uow.Save(); 
            return true;
        }
    }
}

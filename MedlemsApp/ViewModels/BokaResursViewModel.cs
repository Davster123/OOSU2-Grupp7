using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controller;
using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class BokaResursViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();
        private readonly ResursController _resursController;
        private readonly Medlem _inloggadMedlem;

        [ObservableProperty] private ObservableCollection<Resurs> _resurser;
        [ObservableProperty] private ObservableCollection<Utrustning> _utrustningsAlternativ;

        [ObservableProperty] private Resurs _valdResurs;
        [ObservableProperty] private Utrustning _valdUtrustning;
        [ObservableProperty] private DateTime _valdatum = DateTime.Now;
        [ObservableProperty] private string _startTid = "08:00";
        [ObservableProperty] private string _slutTid = "17:00";
        [ObservableProperty] private string _deltagare = "1";

        public BokaResursViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            _resursController = new ResursController(_uow);
            LaddaInitialData();
        }

        private void LaddaInitialData()
        {
            // Hämtar endast resurser med status "Tillgänglig"
            var tillgängliga = _resursController.HämtaTillgängligaResurser();
            Resurser = new ObservableCollection<Resurs>(tillgängliga ?? new System.Collections.Generic.List<Resurs>());

            // Startar med tom utrustningslista tills en resurs väljs
            UtrustningsAlternativ = new ObservableCollection<Utrustning>();
        }

        partial void OnValdResursChanged(Resurs value)
        {
            if (value != null)
            {
                // Hämtar utrustning kopplad till just denna resurs via dess ResursID
                var utr = _uow.UtrustningRepository.Find(u => u.ResursID == value.ResursID).ToList();
                UtrustningsAlternativ = new ObservableCollection<Utrustning>(utr);
            }
        }

        [RelayCommand]
        private void Boka()
        {
            if (ValdResurs == null)
            {
                MessageBox.Show("Välj en resurs!");
                return;
            }

            try
            {
                var nyBokning = new Bokning
                {
                    MedlemID = _inloggadMedlem.MedlemID,
                    ResursID = ValdResurs.ResursID,
                    UtrustningID = ValdUtrustning?.UtrustningID,
                    Deltagare = Deltagare,
                    Datum = Valdatum,
                    Starttid = TimeSpan.Parse(StartTid),
                    Sluttid = TimeSpan.Parse(SlutTid)
                };

                // Lägg till bokning
                _uow.BokningRepository.Add(nyBokning);

                // Ge medlemmen 10 poäng
                _inloggadMedlem.Poäng += 10;
                _uow.MedlemRepository.Update(_inloggadMedlem);

                // Spara allt
                _uow.Save();

                MessageBox.Show("Bokningen är registrerad!");

                StängFönster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel: " + ex.Message);
            }
        }

        [RelayCommand]
        private void Avbryt() => StängFönster();

        private void StängFönster()
        {
            foreach (Window w in Application.Current.Windows)
                if (w.DataContext == this) w.Close();
        }
    }
}

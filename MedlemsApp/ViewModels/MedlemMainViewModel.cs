using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class MedlemMainViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();

        [ObservableProperty]
        private Medlem _inloggadMedlem;

        // Nya egenskaper för statistiken
        [ObservableProperty]
        private double _totalTimmar;

        [ObservableProperty]
        private string _mestAnvandaResurs;

        public MedlemMainViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            BeräknaStatistik();
        }

        private void BeräknaStatistik()
        {
            // Hämtar alla bokningar för den inloggade medlemmen och inkluderar Resurs-datan
            var minaBokningar = _uow.BokningRepository
                .GetAllWithIncludes(b => b.Resurs)
                .Where(b => b.MedlemID == InloggadMedlem.MedlemID)
                .ToList();

            if (minaBokningar.Any())
            {
                // 1. Beräkna totalt antal timmar
                TotalTimmar = minaBokningar.Sum(b => (b.Sluttid - b.Starttid).TotalHours);

                // 2. Hitta den mest använda resurstypen
                MestAnvandaResurs = minaBokningar
                    .GroupBy(b => b.Resurs.Typ)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault() ?? "Ingen data";
            }
            else
            {
                TotalTimmar = 0;
                MestAnvandaResurs = "Inga bokningar än";
            }
        }

        [RelayCommand]
        private void VisaProfil()
        {
            var profilView = new Views.MedlemProfilView();
            profilView.DataContext = new MedlemProfilViewModel(InloggadMedlem);
            profilView.ShowDialog();

            // Tvinga UI att uppdatera bindingen ifall profilbild eller namn har ändrats
            OnPropertyChanged(nameof(InloggadMedlem));
        }

        [RelayCommand]
        private void VisaBokning()
        {
            var bokaView = new Views.BokaResursView();
            bokaView.DataContext = new BokaResursViewModel(InloggadMedlem);
            bokaView.ShowDialog();
        }

        [RelayCommand]
        private void VisaHistorik()
        {
            var historikView = new Views.BokningsHistorikView();
            historikView.DataContext = new BokningsHistorikViewModel(InloggadMedlem);
            historikView.ShowDialog();
        }

        [RelayCommand]
        private void LoggaUt()
        {
            MainWindow startFönster = new MainWindow();
            startFönster.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Views.MedlemMainView)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}

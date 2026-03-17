using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Datalager;
using Entiteter;
using System.Linq;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class MedlemMainViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();

        [ObservableProperty]
        private Medlem _inloggadMedlem;

        [ObservableProperty]
        private double _totalTimmar;

        [ObservableProperty]
        private string _mestAnvandaResurs;

        public string RabattInfo
        {
            get
            {
                int kvar = 100 - InloggadMedlem.Poäng;

                if (kvar <= 0)
                    return "Du har uppnått 15% rabatt i receptionen!";

                return $"Du har {kvar} poäng kvar till 15% rabatt.";
            }
        }

        public MedlemMainViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            BeräknaStatistik();
        }

        private void BeräknaStatistik()
        {
            var minaBokningar = _uow.BokningRepository
                .GetAllWithIncludes(b => b.Resurs)
                .Where(b => b.MedlemID == InloggadMedlem.MedlemID)
                .ToList();

            if (minaBokningar.Any())
            {
                TotalTimmar = minaBokningar.Sum(b => (b.Sluttid - b.Starttid).TotalHours);

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

            OnPropertyChanged(nameof(InloggadMedlem));
            OnPropertyChanged(nameof(RabattInfo));
        }

        [RelayCommand]
        private void VisaBokning()
        {
            var bokaView = new Views.BokaResursView();
            bokaView.DataContext = new BokaResursViewModel(InloggadMedlem);
            bokaView.ShowDialog();

            BeräknaStatistik();   // 

            OnPropertyChanged(nameof(InloggadMedlem));
            OnPropertyChanged(nameof(RabattInfo));
        }

        [RelayCommand]
        private void VisaHistorik()
        {
            var historikView = new Views.BokningsHistorikView();
            historikView.DataContext = new BokningsHistorikViewModel(InloggadMedlem);
            historikView.ShowDialog();

            BeräknaStatistik();

            OnPropertyChanged(nameof(InloggadMedlem));
            OnPropertyChanged(nameof(RabattInfo));
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

        [RelayCommand]
        private void VisaCommunity()
        {
            var communityView = new Views.CommunityView();
            communityView.DataContext = new CommunityViewModel(InloggadMedlem);
            communityView.ShowDialog();
        }
    }
}
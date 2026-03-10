using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        [ObservableProperty]
        private Medlem _inloggadMedlem;

        public MedlemMainViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
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

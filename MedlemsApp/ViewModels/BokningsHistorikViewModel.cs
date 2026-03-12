using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class BokningsHistorikViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();
        private readonly Medlem _inloggadMedlem;

        private List<Bokning> _allaBokningar;

        [ObservableProperty]
        private ObservableCollection<Bokning> _minaBokningar;

        [ObservableProperty]
        private Bokning _valdBokning;

        [ObservableProperty]
        private List<string> _resursTyper;

        [ObservableProperty]
        private string _valdResursTyp;

        public BokningsHistorikViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;

            ResursTyper = new List<string>
            {
                "Alla",
                "Mötesrum",
                "Datorsal",
                "Konferenssal"
               
            };

            LaddaBokningar();

            ValdResursTyp = "Alla";

            
        }

        private void LaddaBokningar()
        {
            var allaBokningar = _uow.BokningRepository.GetAllWithIncludes(b => b.Resurs);

            _allaBokningar = allaBokningar
                .Where(b => b.MedlemID == _inloggadMedlem.MedlemID)
                .ToList();

            MinaBokningar = new ObservableCollection<Bokning>(_allaBokningar);
        }

        partial void OnValdResursTypChanged(string value)
        {
            FiltreraBokningar();
        }

        private void FiltreraBokningar()
        {
            if (ValdResursTyp == "Alla")
            {
                MinaBokningar = new ObservableCollection<Bokning>(_allaBokningar);
            }
            else
            {
                var filtrerade = _allaBokningar
                    .Where(b => b.Resurs != null && b.Resurs.Typ == ValdResursTyp)
                    .ToList();

                MinaBokningar = new ObservableCollection<Bokning>(filtrerade);
            }
        }

        [RelayCommand]
        private void Avboka()
        {
            if (ValdBokning == null) return;

            var svar = MessageBox.Show(
                $"Är du säker på att du vill avboka {ValdBokning.Resurs?.Namn}?",
                "Bekräfta avbokning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (svar == MessageBoxResult.Yes)
            {
                _uow.BokningRepository.Remove(ValdBokning);
                _uow.Save();

                MinaBokningar.Remove(ValdBokning);
                _allaBokningar.Remove(ValdBokning);

                MessageBox.Show("Bokningen är nu borttagen.");
            }
        }

        [RelayCommand]
        private void ÄndraBokning()
        {
            if (ValdBokning == null) return;

            var editView = new Views.BokaResursView();

            editView.DataContext = new BokaResursViewModel(_inloggadMedlem);

            editView.ShowDialog();

            LaddaBokningar();
        }

        [RelayCommand]
        private void Tillbaka()
        {
            foreach (Window w in Application.Current.Windows)
                if (w.DataContext == this)
                    w.Close();
        }
    }
}
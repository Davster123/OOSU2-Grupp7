using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class BokningsHistorikViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();
        private readonly Medlem _inloggadMedlem;

        [ObservableProperty]
        private ObservableCollection<Bokning> _minaBokningar;

        [ObservableProperty]
        private Bokning _valdBokning;

        public BokningsHistorikViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            LaddaBokningar();
        }

        private void LaddaBokningar()
        {
            // Vi hämtar alla bokningar och tvingar EF att även hämta Resurs-objektet
            var allaBokningar = _uow.BokningRepository.GetAllWithIncludes(b => b.Resurs);

            // Filtrera så vi bara ser den inloggade medlemmens bokningar
            var filtrerade = allaBokningar.Where(b => b.MedlemID == _inloggadMedlem.MedlemID).ToList();

            MinaBokningar = new ObservableCollection<Bokning>(filtrerade);
        }

        [RelayCommand]
        private void Avboka()
        {
            if (ValdBokning == null) return;

            var svar = MessageBox.Show($"Är du säker på att du vill avboka {ValdBokning.Resurs?.Namn}?",
                "Bekräfta avbokning", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (svar == MessageBoxResult.Yes)
            {
                _uow.BokningRepository.Remove(ValdBokning);
                _uow.Save();
                MinaBokningar.Remove(ValdBokning);
                MessageBox.Show("Bokningen är nu borttagen.");
            }
        }

        [RelayCommand]
        private void ÄndraBokning()
        {
            if (ValdBokning == null) return;

            // Här återanvänder vi BokaResursView men skickar in den valda bokningen för redigering
            // För enkelhetens skull i detta exempel öppnar vi bokningsfönstret:
            var editView = new Views.BokaResursView();
            // Man kan utöka BokaResursViewModel för att ta emot en befintlig bokning
            editView.DataContext = new BokaResursViewModel(_inloggadMedlem);
            editView.ShowDialog();

            LaddaBokningar(); // Uppdatera listan efteråt
        }

        [RelayCommand]
        private void Tillbaka()
        {
            foreach (Window w in Application.Current.Windows)
                if (w.DataContext == this) w.Close();
        }
    }
}

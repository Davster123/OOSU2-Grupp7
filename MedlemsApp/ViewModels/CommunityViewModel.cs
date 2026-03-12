using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Datalager;
using Entiteter;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class CommunityViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();
        private readonly Medlem _inloggadMedlem;

        [ObservableProperty]
        private ObservableCollection<Medlem> _medlemmar;

        public CommunityViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            LaddaMedlemmar();
        }

        private void LaddaMedlemmar()
        {
            var allaMedlemmar = _uow.MedlemRepository
                .GetAll()
                .Where(m => m.MedlemID != _inloggadMedlem.MedlemID)
                .ToList();

            Medlemmar = new ObservableCollection<Medlem>(allaMedlemmar);
        }

        [RelayCommand]
        private void Stäng()
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.DataContext == this)
                {
                    w.Close();
                    break;
                }
            }
        }
    }
}

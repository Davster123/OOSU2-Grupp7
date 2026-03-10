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
    public partial class MedlemProfilViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();

        [ObservableProperty] private Medlem _inloggadMedlem;
        [ObservableProperty] private string _email;
        [ObservableProperty] private string _telefon;
        [ObservableProperty] private string _nyttLosenord;

        public MedlemProfilViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            Email = medlem.Email;
            Telefon = medlem.Telefonnummer;
        }

        [RelayCommand]
        private void BytBild()
        {
            // Här kan ni implementera Microsoft.Win32.OpenFileDialog för att välja bild
            MessageBox.Show("Här öppnas filväljaren för att välja en ny profilbild!");
        }

        [RelayCommand]
        private void Spara()
        {
            try
            {
                InloggadMedlem.Email = Email;
                InloggadMedlem.Telefonnummer = Telefon;

                if (!string.IsNullOrWhiteSpace(NyttLosenord))
                    InloggadMedlem.Losenord = NyttLosenord;

                _uow.MedlemRepository.Update(InloggadMedlem);
                _uow.Save();
                MessageBox.Show("Profilen har uppdaterats!");
            }
            catch (System.Exception ex) { MessageBox.Show("Fel: " + ex.Message); }
        }
    }
}

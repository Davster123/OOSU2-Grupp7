using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Datalager;
using Entiteter;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                // Fixar CS0029 genom att använda byte[] (kräver ändring i Medlem.cs)
                byte[] bildBytes = File.ReadAllBytes(openFileDialog.FileName);
                InloggadMedlem.Profilbild = bildBytes;

                OnPropertyChanged(nameof(InloggadMedlem));
            }
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
                StängFönster();
            }
            catch (Exception ex) { MessageBox.Show("Fel: " + ex.Message); }
        }

        //Kommando för Tillbaka-knappen
        [RelayCommand]
        private void Tillbaka()
        {
            StängFönster();
        }

        private void StängFönster()
        {
            foreach (Window w in Application.Current.Windows)
                if (w.DataContext == this) w.Close();
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controller;
using Datalager;
using System;
using System.Windows;

namespace MedlemsApp.ViewModels
{    
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly MedlemsController _medlemsController;
        private readonly UnitOfWork _uow;

        // Dessa attribut skapar automatiskt publika egenskaper (Namn, Email osv.) 
        [ObservableProperty]
        private string _namn = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _telefon = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        public RegisterViewModel()
        {
            // Initiera datalager och controller
            _uow = new UnitOfWork();
            _medlemsController = new MedlemsController(_uow);
        }

        [RelayCommand]
        private void Register()
        {
            string felmeddelande;
       
            bool success = _medlemsController.LäggTillMedlem(
                Namn,
                Telefon,
                Email,
                "Flex",
                "Obetald",
                Password, 
                out felmeddelande
            );

            if (success)
            {
                MessageBox.Show("Kontot har skapats framgångsrikt! Du kan nu logga in.", "Registrering lyckades", MessageBoxButton.OK, MessageBoxImage.Information);

                RensaFält();
            }
            else
            {
                MessageBox.Show(felmeddelande, "Fel vid registrering", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RensaFält()
        {
            Namn = string.Empty;
            Email = string.Empty;
            Telefon = string.Empty;
            Password = string.Empty;
        }

        [RelayCommand]
        private void Tillbaka()
        {
            MainWindow huvudFönster = new MainWindow();
            huvudFönster.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Views.RegisterView)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedlemsApp.Views;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        // Öppnar inloggning för medlemmar
        [RelayCommand]
        private void VisaLogin()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            StängMainWindow();
        }

        // Öppnar registreringssidan för medlemmar
        [RelayCommand]
        private void VisaRegister()
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
            StängMainWindow();
        }

        // Öppnar personalens inloggningsfönster (från ert andra projekt)
        [RelayCommand]
        private void VisaPersonalLogin()
        {
            // Kontrollera att namespacet stämmer med er gamla MainWindow-fil
            // Här antar vi att den heter MainWindow och ligger i Presentationslager_WPF_
            Presentationslager_WPF_.MainWindow personalFönster = new Presentationslager_WPF_.MainWindow();
            personalFönster.Show();
            StängMainWindow();
        }

        private void StängMainWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controller;
using Datalager;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty; 

        public LoginViewModel()
        {
            _uow = new UnitOfWork();
        }

        [RelayCommand]
        private void Login()
        {
            // Vi hämtas medlemmen baserat på e-post
            var medlem = _uow.MedlemRepository.FirstOrDefault(m => m.Email == Email);


            if (medlem != null)
            {
                MessageBox.Show($"Välkommen {medlem.Namn}!");
            }
            else
            {
                MessageBox.Show("Felaktig e-post eller lösenord.");
            }
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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controller;
using Datalager;
using System.Windows;
using System.Windows.Controls;

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
        private void Login(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                Password = passwordBox.Password;
            }

            var medlem = _uow.MedlemRepository.FirstOrDefault(m => m.Email == Email && m.Losenord == Password);

            if (medlem != null)
            {
                var mainView = new Views.MedlemMainView();
                mainView.DataContext = new MedlemMainViewModel(medlem);
                mainView.Show();

                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is Views.LoginView)?.Close();
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
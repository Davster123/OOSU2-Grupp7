using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedlemsApp.Views;

namespace MedlemsApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [RelayCommand]
        private void VisaLogin()
        {
            LoginView loginFönster = new LoginView();
            loginFönster.Show();        
        }

        [RelayCommand]
        private void VisaRegister()
        {
            RegisterView registerFönster = new RegisterView();
            registerFönster.Show();
        }
    }
}
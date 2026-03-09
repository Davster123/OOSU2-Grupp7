using Datalager;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentationslager_WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void loggaInButton_Click(object sender, RoutedEventArgs e)
        {
            using var uow = new UnitOfWork();
            string namn = användarnamnTexbox.Text;
            string lösen = lösenordTextbox.Password;

            //Här kontrollerar vi ifall personen som ska logga in finns registrerad i personaltabellen
            var anställd = uow.PersonalRepository.FirstOrDefault(p => p.Namn == namn && p.Losenord == lösen);

            if (anställd != null)
            {
                MessageBox.Show($"Välkommen {anställd.Namn} ({anställd.Roll})!");
                new PersonalMeny().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Fel användarnamn eller lösenord.");
            }


        }
    }
}
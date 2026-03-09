using Controller;
using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentationslager_WPF_
{
    /// <summary>
    /// Interaction logic for UppdateraMedlemmar.xaml
    /// </summary>
    public partial class UppdateraMedlemmar : Window
    {
        private readonly UnitOfWork _uow;
        private readonly MedlemsController _controller;
        public UppdateraMedlemmar()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _controller = new MedlemsController(_uow);


            nivaComboBox.ItemsSource = new List<string> { "Flex", "Fast", "Företag" };
            statusComboBox.ItemsSource = new List<string> { "Betald", "Obetald" };

            // Ladda alla medlemmar i listan högst upp
            medlemComboBox.ItemsSource = _controller.HämtaAllaMedlemmar();
        }
        private void medlemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // När en medlem väljs i listan fylls alla textfält automatiskt med den personens data.
            if (medlemComboBox.SelectedItem is Medlem valdMedlem)
            {
                namnTextBox.Text = valdMedlem.Namn;
                emailTextBox.Text = valdMedlem.Email;
                telefonTextBox.Text = valdMedlem.Telefonnummer;
                nivaComboBox.SelectedItem = valdMedlem.MedlemskapsNivå;
                statusComboBox.SelectedItem = valdMedlem.Betalstatus;
            }
        }
        private void sparaButton_Click(object sender, RoutedEventArgs e)
        {
            if (medlemComboBox.SelectedItem is Medlem valdMedlem)
            {
                // Uppdatera objektet med nya värden
                valdMedlem.Namn = namnTextBox.Text;
                valdMedlem.Email = emailTextBox.Text;
                valdMedlem.Telefonnummer = telefonTextBox.Text;
                valdMedlem.MedlemskapsNivå = nivaComboBox.SelectedItem?.ToString();
                valdMedlem.Betalstatus = statusComboBox.SelectedItem?.ToString();

                // Spara via controllern
                _controller.UppdateraMedlem(valdMedlem);

                MessageBox.Show("Medlem uppdaterad!");
                _uow.Dispose();
                HanteraMedlemar meny = new HanteraMedlemar();
                meny.Show();
                this.Close();
            }
        }

        private void tillbakaButton_Click(object sender, RoutedEventArgs e)
        {
            _uow.Dispose();
            HanteraMedlemar meny = new HanteraMedlemar();
            meny.Show();
            this.Close();
        }

        private void raderaMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            if (medlemComboBox.SelectedItem is Medlem valdMedlem)
            {
                // Bekräfta radering med användaren
                MessageBoxResult resultat = MessageBox.Show(
                    $"Är du säker på att du vill ta bort {valdMedlem.Namn}?",
                    "Bekräfta radering",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultat == MessageBoxResult.Yes)
                {
                    // Anropa controllern för att ta bort från databasen
                    _controller.TaBortMedlem(valdMedlem);

                    MessageBox.Show("Medlemmen har tagits bort.");

                    // Uppdatera listan i ComboBoxen så den borttagna medlemmen försvinner
                    medlemComboBox.ItemsSource = _controller.HämtaAllaMedlemmar();

                    // Tömmer UI-fälten
                    RensaFält();

                }
            }
            else
            {
                MessageBox.Show("Vänligen välj en medlem i listan först.");
            }
        }

        // Hjälpmetod för att rensa textrutorna
        private void RensaFält()
        {
            namnTextBox.Clear();
            emailTextBox.Clear();
            telefonTextBox.Clear();
            nivaComboBox.SelectedIndex = -1;
            statusComboBox.SelectedIndex = -1;
        }
    }
}

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
    /// Interaction logic for RegisteraResursbokning.xaml
    /// </summary>
    public partial class RegisteraResursbokning : Window
    {
        private readonly UnitOfWork _uow;
        private readonly BokningsController _bokningsController;
        private readonly MedlemsController _medlemsController;
        private readonly ResursController _resursController;
        public RegisteraResursbokning()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _bokningsController = new BokningsController(_uow);
            _medlemsController = new MedlemsController(_uow);
            _resursController = new ResursController(_uow);

            // Sätt dagens datum som standardval
            BokningsdatumPicker.SelectedDate = DateTime.Now;
            LaddaData();
        }
        private void LaddaData()
        {
            // Hämta alla medlemmar som vanligt
            medlemComboBox.ItemsSource = _medlemsController.HämtaAllaMedlemmar();

            // Anropa den nya metoden som bara returnerar tillgängliga resurser
            resursComboBox.ItemsSource = _resursController.HämtaTillgängligaResurser();
        }
        private void resursComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (resursComboBox.SelectedItem is Resurs valdResurs)
            {
                // Hämtar utrustning som tillhör den valda resursen via Repositoryt
                var tillhörandeUtrustning = _uow.UtrustningRepository.Find(u => u.ResursID == valdResurs.ResursID).ToList();
                utrustningComboBox.ItemsSource = tillhörandeUtrustning;
            }
        }

        private void registreraBokningButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kontrollera att användaren valt något i rullistorna
                if (resursComboBox.SelectedItem is not Resurs valdResurs)
                {
                    MessageBox.Show("Vänligen välj en resurs.");
                    return;
                }

                if (medlemComboBox.SelectedItem is not Medlem valdMedlem)
                {
                    MessageBox.Show("Vänligen välj en medlem.");
                    return;
                }

                if (BokningsdatumPicker.SelectedDate == null)
                {
                    MessageBox.Show("Vänligen välj ett datum.");
                    return;
                }

                DateTime datum = BokningsdatumPicker.SelectedDate.Value;
                TimeSpan start = TimeSpan.Parse(StarttidTextBox.Text);
                TimeSpan slut = TimeSpan.Parse(SluttidTextBox.Text);

                Utrustning? valdUtrustning = utrustningComboBox.SelectedItem as Utrustning;
                string deltagareInfo = deltagareTextbox.Text;

                bool resultat = _bokningsController.SkapaBokning(
                    valdMedlem.MedlemID,
                    valdResurs.ResursID,
                    valdUtrustning?.UtrustningID,
                    deltagareInfo,
                    datum,
                    start,
                    slut,
                    out string felmeddelande
                );

                if (!resultat)
                {
                    MessageBox.Show(felmeddelande);
                    return;
                }
                string info = valdUtrustning != null
                    ? $"\nBokad utrustning: {valdUtrustning.ArtikelNamn}"
                    : "";

                MessageBox.Show("Bokningen har registrerats!");
                _uow.Dispose();
                HanteraResursbokningar meny = new HanteraResursbokningar();
                meny.Show();
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Vänligen ange tider i formatet HH:mm (t.ex. 14:00).");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel uppstod: {ex.Message}");
            }
        }

        private void avbrytButton_Click(object sender, RoutedEventArgs e)
        {
            _uow?.Dispose();
            HanteraResursbokningar meny = new HanteraResursbokningar();
            meny.Show();
            this.Close();
        }
    }
}

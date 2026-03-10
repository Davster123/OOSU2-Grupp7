using Controller;
using Datalager;
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
    /// Interaction logic for RegistreraMedlemmar.xaml
    /// </summary>
    public partial class RegistreraMedlemmar : Window
    {
        private readonly UnitOfWork _uow;
        private readonly MedlemsController _controller;
        public RegistreraMedlemmar()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _controller = new MedlemsController(_uow);

            medlemskapsnivåComboBox.ItemsSource = new List<string>
            {
                "Välj nivå",
                "Flex",
                "Fast",
                "Företag"
            };

            medlemskapsnivåComboBox.SelectedIndex = 0;


            betalningsStatusComboBox.ItemsSource = new List<string>
            {
               "Välj status",
                "Betald",
                "Obetald"
            };

            betalningsStatusComboBox.SelectedIndex = 0;
        }
        private void registreraMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            // Hämtar data från textrutor och dropdown-menyer.
            string namn = namnTextBox.Text;
            string telefon = telefonnummerTextBox.Text;
            string email = emailTextBox.Text;


            string medlemskapsnivå = medlemskapsnivåComboBox.SelectedItem?.ToString();
            string betalstatus = betalningsStatusComboBox.SelectedItem?.ToString();

            // Anropar kontrollern som utför validering

            bool resultat = _controller.LäggTillMedlem(
                namn,
                telefon,
                email,
                medlemskapsnivå,
                betalstatus,
                "1234", 
                out string felmeddelande
                );

            if (!resultat)
            {
                MessageBox.Show(felmeddelande);
                return;
            }

            MessageBox.Show("Medlem registrerad!");
            _uow.Dispose();

            HanteraMedlemar meny = new HanteraMedlemar();
            meny.Show();

            this.Close();

        }

        private void avbrytRegistreringButton_Click(object sender, RoutedEventArgs e)
        {
            _uow?.Dispose();

            HanteraMedlemar meny = new HanteraMedlemar();
            meny.Show();
            this.Close();
        }
    }
}

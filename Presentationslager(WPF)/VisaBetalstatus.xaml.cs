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
    /// Interaction logic for VisaBetalstatus.xaml
    /// </summary>
    public partial class VisaBetalstatus : Window
    {
        private readonly UnitOfWork _uow;
        private readonly BetalningsController _betalningsController;
        private readonly MedlemsController _medlemsController;
        public VisaBetalstatus()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _betalningsController = new BetalningsController(_uow);
            _medlemsController = new MedlemsController(_uow);

            LaddaMedlemmar();
        }
        private void LaddaMedlemmar()
        {
            medlemComboBox.ItemsSource = _medlemsController.HämtaAllaMedlemmar();
        }

        private void medlemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UppdateraTabell();
        }

        private void UppdateraTabell()
        {
            if (medlemComboBox.SelectedItem is Medlem valdMedlem)
            {
                // Hämta alla betalningar kopplade till den valda medlemmen
                betalningarDataGrid.ItemsSource = _betalningsController.HämtaBetalningarFörMedlem(valdMedlem.MedlemID);
            }
        }
        private void registreraBetalningButton_Click(object sender, RoutedEventArgs e)
        {
            if (betalningarDataGrid.SelectedItem is Betalning valdBetalning)
            {
                if (valdBetalning.Status == "Betald")
                {
                    MessageBox.Show("Denna faktura är redan markerad som betald.");
                    return;
                }

                // Uppdatera status via controllern
                _betalningsController.RegistreraBetalning(valdBetalning.BetalningID);
                MessageBox.Show("Betalningen har registrerats!");

                UppdateraTabell(); // Ladda om tabellen för att visa ändringen
            }
            else
            {
                MessageBox.Show("Vänligen välj en betalning i tabellen.");
            }
        }
        private void tillbakaButton_Click(object sender, RoutedEventArgs e)
        {
            _uow.Dispose();
            VisaStatistik meny = new VisaStatistik();
            meny.Show();
            this.Close();
        }
    }
}

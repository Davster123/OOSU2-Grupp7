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
    /// Interaction logic for UppdateraTillgänglighet.xaml
    /// </summary>
    public partial class UppdateraTillgänglighet : Window
    {
        private readonly UnitOfWork _uow;
        private readonly ResursController _controller;
        public UppdateraTillgänglighet()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _controller = new ResursController(_uow);

            nyStatusComboBox.ItemsSource = new List<string> { "Tillgänglig", "Ej tillgänglig", "Underhåll" };

            LaddaResurser();
        }
        private void LaddaResurser()
        {
            resursComboBox.ItemsSource = _controller.HämtaAllaResurser();
        }

        private void resursComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (resursComboBox.SelectedItem is Resurs valdResurs)
            {
                nuvarandeStatusText.Text = valdResurs.Status;
                nyStatusComboBox.SelectedItem = valdResurs.Status;
            }
        }
        private void sparaButton_Click(object sender, RoutedEventArgs e)
        {
            if (resursComboBox.SelectedItem is Resurs valdResurs)
            {
                string nyStatus = nyStatusComboBox.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(nyStatus))
                {
                    MessageBox.Show("Vänligen välj en ny status.");
                    return;
                }

                _controller.UppdateraResursStatus(valdResurs.ResursID, nyStatus);
                MessageBox.Show($"Status för {valdResurs.Namn} har uppdaterats till {nyStatus}.");

                _uow.Dispose();
                HanteraResurs meny = new HanteraResurs();
                meny.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Vänligen välj en resurs i listan.");
            }
        }
        private void avbrytButton_Click(object sender, RoutedEventArgs e)
        {
            _uow.Dispose();
            HanteraResurs meny = new HanteraResurs();
            meny.Show();
            this.Close();
        }
    }
}

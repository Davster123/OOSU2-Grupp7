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
    /// Interaction logic for VisaResursbokningar.xaml
    /// </summary>
    public partial class VisaResursbokningar : Window
    {
        private readonly UnitOfWork _uow;
        private readonly BokningsController _controller;
        public VisaResursbokningar()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _controller = new BokningsController(_uow);

            // Ladda in data i tabellen
            LaddaBokningar();
        }
        private void LaddaBokningar()
        {
            try
            {
                // Hämtar bokningar via controllern
                var bokningsLista = _controller.HämtaAllaBokningar();

                // Koppla listan till DataGrid
                bokningarDataGrid.ItemsSource = bokningsLista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunde inte ladda bokningar: " + ex.Message);
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

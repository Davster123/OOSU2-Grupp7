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
    /// Interaction logic for VisaMedlemmar.xaml
    /// </summary>
    public partial class VisaMedlemmar : Window
    {
        private readonly UnitOfWork _uow;
        private readonly MedlemsController _controller;
        public VisaMedlemmar()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _controller = new MedlemsController(_uow);
            LaddaMedlemmar();
        }
        private void LaddaMedlemmar()
        {
            // Hämta listan via controllern
            var medlemsLista = _controller.HämtaAllaMedlemmar();

            // Detta kopplar listan till datagriden
            medlemmarDataGrid.ItemsSource = medlemsLista;
        }

        private void tillbakaButton_Click(object sender, RoutedEventArgs e)
        {
            _uow.Dispose();
            HanteraMedlemar meny = new HanteraMedlemar();
            meny.Show();
            this.Close();
        }
    }
}

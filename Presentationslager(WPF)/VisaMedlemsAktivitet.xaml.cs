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
    /// Interaction logic for VisaMedlemsAktivitet.xaml
    /// </summary>
    public partial class VisaMedlemsAktivitet : Window
    {

        private readonly UnitOfWork _uow;
        private readonly MedlemsController _medlemsController;
        private readonly BokningsController _bokningsController;
        private readonly BetalningsController _betalningsController;
        public VisaMedlemsAktivitet()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _medlemsController = new MedlemsController(_uow);
            _bokningsController = new BokningsController(_uow);
            _betalningsController = new BetalningsController(_uow);

            LaddaMedlemmar();
        }
        private void LaddaMedlemmar()
        {
            medlemComboBox.ItemsSource = _medlemsController.HämtaAllaMedlemmar();
        }

        private void medlemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (medlemComboBox.SelectedItem is Medlem valdMedlem)
            {
                // Hämta betalningar via BetalningsController
                betalningarDataGrid.ItemsSource = _betalningsController.HämtaBetalningarFörMedlem(valdMedlem.MedlemID);

                // Hämta bokningar via BokningsController och filtrera på medlem
                var allaBokningar = _bokningsController.HämtaAllaBokningar();
                bokningarDataGrid.ItemsSource = allaBokningar.Where(b => b.MedlemID == valdMedlem.MedlemID).ToList();
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

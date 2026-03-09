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
    /// Interaction logic for VisaStatistik.xaml
    /// </summary>
    public partial class VisaStatistik : Window
    {
        public VisaStatistik()
        {
            InitializeComponent();
        }
        private void TillbakaButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalMeny meny = new PersonalMeny();
            meny.Show();
            this.Close();
        }

        private void visaBetalstatusButton_Click(object sender, RoutedEventArgs e)
        {
            VisaBetalstatus meny = new VisaBetalstatus();
            meny.Show();
            this.Close();
        }

        private void visaResursbokningButton_Click(object sender, RoutedEventArgs e)
        {
            VisaResursbokningar meny = new VisaResursbokningar();
            meny.Show();
            this.Close();
        }

        private void visaMedlemsAktivitet_Click(object sender, RoutedEventArgs e)
        {
            VisaMedlemsAktivitet meny = new VisaMedlemsAktivitet();
            meny.Show();
            this.Close();
        }
    }
}

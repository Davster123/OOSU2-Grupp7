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
    /// Interaction logic for PersonalMeny.xaml
    /// </summary>
    public partial class PersonalMeny : Window
    {
        public PersonalMeny()
        {
            InitializeComponent();
        }
        private void hanteraMedlemmarButton_Click(object sender, RoutedEventArgs e)
        {
            HanteraMedlemar meny = new HanteraMedlemar();
            meny.Show();
            this.Close();
        }

        private void hanteraResursbokningButton_Click(object sender, RoutedEventArgs e)
        {
            HanteraResursbokningar meny = new HanteraResursbokningar();
            meny.Show();
            this.Close();
        }

        private void hanteraResurs_Click(object sender, RoutedEventArgs e)
        {
            HanteraResurs meny = new HanteraResurs();
            meny.Show();
            this.Close();
        }

        private void visaStatistikButton_Click(object sender, RoutedEventArgs e)
        {
            VisaStatistik meny = new VisaStatistik();
            meny.Show();
            this.Close();
        }
    }
}

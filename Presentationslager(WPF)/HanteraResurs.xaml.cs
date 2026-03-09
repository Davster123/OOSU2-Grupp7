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
    /// Interaction logic for HanteraResurs.xaml
    /// </summary>
    public partial class HanteraResurs : Window
    {
        public HanteraResurs()
        {
            InitializeComponent();
        }
        private void tillbakaButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalMeny meny = new PersonalMeny();
            meny.Show();
            this.Close();
        }

        private void UppdateraResursButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraTillgänglighet meny = new UppdateraTillgänglighet();
            meny.Show();
            this.Close();
        }
    }
}

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
    /// Interaction logic for HanteraResursbokningar.xaml
    /// </summary>
    public partial class HanteraResursbokningar : Window
    {
        public HanteraResursbokningar()
        {
            InitializeComponent();
        }
        private void TillbakaButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalMeny meny = new PersonalMeny();
            meny.Show();
            this.Close();
        }

        private void uppdateraResursbokningButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraResursbokning meny = new UppdateraResursbokning();
            meny.Show();
            this.Close();
        }

        private void registreraResursbokningButton_Click(object sender, RoutedEventArgs e)
        {
            RegisteraResursbokning meny = new RegisteraResursbokning();
            meny.Show();
            this.Close();
        }
    }
}

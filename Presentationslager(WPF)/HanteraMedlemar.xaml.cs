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
    /// Interaction logic for HanteraMedlemar.xaml
    /// </summary>
    public partial class HanteraMedlemar : Window
    {
        private UnitOfWork _uow;
        private MedlemsController _controller;
        public HanteraMedlemar()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _controller = new MedlemsController(_uow);
        }
        private void visaMedlemmarButton_Click(object sender, RoutedEventArgs e)
        {
            VisaMedlemmar meny = new VisaMedlemmar();
            meny.Show();
            this.Close();
        }

        private void uppdateraMedlemmarButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraMedlemmar meny = new UppdateraMedlemmar();
            meny.Show();
            this.Close();
        }

        private void registreraMedlemmarButton_Click(object sender, RoutedEventArgs e)
        {
            RegistreraMedlemmar meny = new RegistreraMedlemmar();
            meny.Show();
            this.Close();
        }

        private void återgåButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalMeny meny = new PersonalMeny();
            meny.Show();
            this.Close();
        }
    }
}

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
    /// Interaction logic for UppdateraResursbokning.xaml
    /// </summary>
    public partial class UppdateraResursbokning : Window
    {
        private readonly UnitOfWork _uow;
        private readonly BokningsController _controller;
        public UppdateraResursbokning()
        {
            InitializeComponent();
            _uow = new UnitOfWork();
            _controller = new BokningsController(_uow);

            LaddaBokningar();
        }
        private void LaddaBokningar() //visar alla bokningar med hjälp av hämtning av controllern
        {
            bokningComboBox.ItemsSource = _controller.HämtaAllaBokningar();
        }
        private void bokningComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bokningComboBox.SelectedItem is Bokning vald)
            {
                resursIdTextBox.Text = vald.ResursID.ToString();
                medlemIdTextBox.Text = vald.MedlemID.ToString();
                datumPicker.SelectedDate = vald.Datum;
                starttidTextBox.Text = vald.Starttid.ToString(@"hh\:mm");
                sluttidTextBox.Text = vald.Sluttid.ToString(@"hh\:mm");
            }
        }

        private void sparaButton_Click(object sender, RoutedEventArgs e)
        {
            if (bokningComboBox.SelectedItem is Bokning vald)
            {
                try
                {
                    vald.ResursID = int.Parse(resursIdTextBox.Text);
                    vald.MedlemID = int.Parse(medlemIdTextBox.Text);
                    vald.Datum = datumPicker.SelectedDate ?? vald.Datum;
                    vald.Starttid = TimeSpan.Parse(starttidTextBox.Text);
                    vald.Sluttid = TimeSpan.Parse(sluttidTextBox.Text);

                    _controller.UppdateraBokning(vald);
                    MessageBox.Show("Bokningen har uppdaterats!");
                    _uow.Dispose();
                    HanteraResursbokningar meny = new HanteraResursbokningar();
                    meny.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fel vid inmatning: " + ex.Message);
                }
            }
        }

        private void taBortButton_Click(object sender, RoutedEventArgs e)
        {
            if (bokningComboBox.SelectedItem is Bokning vald)
            {
                var svar = MessageBox.Show($"Vill du verkligen ta bort bokning {vald.BokningID}?", "Bekräfta", MessageBoxButton.YesNo);
                if (svar == MessageBoxResult.Yes)
                {
                    _controller.TaBortBokning(vald.BokningID);
                    MessageBox.Show("Bokningen har tagits bort.");
                    LaddaBokningar();
                    RensaFält();

                }
            }
        }
        private void RensaFält()
        {
            resursIdTextBox.Clear();
            medlemIdTextBox.Clear();
            datumPicker.SelectedDate = null;
            starttidTextBox.Clear();
            sluttidTextBox.Clear();
            bokningComboBox.SelectedIndex = -1;
        }
        private void avbrytButton_Click(object sender, RoutedEventArgs e)
        {
            _uow.Dispose();
            HanteraResursbokningar meny = new HanteraResursbokningar();
            meny.Show();
            this.Close();
        }
    }
}

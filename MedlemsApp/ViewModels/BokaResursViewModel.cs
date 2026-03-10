using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Datalager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedlemsApp.ViewModels
{
    public partial class BokaResursViewModel : ObservableObject
    {
        private readonly UnitOfWork _uow = new UnitOfWork();
        private readonly Medlem _inloggadMedlem;

        [ObservableProperty] private ObservableCollection<Resurs> _resurser;
        [ObservableProperty] private ObservableCollection<Utrustning> _utrustningsAlternativ;

        [ObservableProperty] private Resurs _valdResurs;
        [ObservableProperty] private Utrustning _valdUtrustning;
        [ObservableProperty] private DateTime _valdatum = DateTime.Now;
        [ObservableProperty] private string _startTid = "08:00";
        [ObservableProperty] private string _slutTid = "17:00";
        [ObservableProperty] private string _deltagare = "1";

        public BokaResursViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            LaddaData();
        }

        private void LaddaData()
        {
            var res = _uow.ResursRepository.GetAll();
            var utr = _uow.UtrustningRepository.GetAll();

            Resurser = res != null ? new ObservableCollection<Resurs>(res) : new ObservableCollection<Resurs>();
            UtrustningsAlternativ = utr != null ? new ObservableCollection<Utrustning>(utr) : new ObservableCollection<Utrustning>();
        }

        [RelayCommand]
        private void Avbryt()
        {
            StängFönster();
        }

        private void StängFönster()
        {
            foreach (Window w in Application.Current.Windows)
                if (w.DataContext == this) w.Close();
        }

        [RelayCommand]
        private void Boka()
        {
            if (ValdResurs == null) { MessageBox.Show("Välj en resurs!"); return; }

            try
            {
                var nyBokning = new Bokning
                {
                    MedlemID = _inloggadMedlem.MedlemID,
                    ResursID = ValdResurs.ResursID,
                    UtrustningID = ValdUtrustning?.UtrustningID,
                    Deltagare = Deltagare,
                    Datum = Valdatum,
                    Starttid = TimeSpan.Parse(StartTid),
                    Sluttid = TimeSpan.Parse(SlutTid)
                };

                _uow.BokningRepository.Add(nyBokning);
                _uow.Save();

                MessageBox.Show($"Bokat: {ValdResurs.Namn}");
                StängFönster();
            }
            catch (Exception ex) { MessageBox.Show("Fel: " + ex.Message); }
        }
    }
}

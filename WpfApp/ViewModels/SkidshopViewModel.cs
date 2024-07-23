using AffärsLager;
using Entiteterna;
using DataLager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Views;
using static Entiteterna.Skidskola;
using static Entiteterna.Utrustning;
using SkidShopView = Entiteterna.SkidShopView;
using static Entiteterna.Skidskola;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WpfApp.ViewModels
{
    public class SkidshopViewModel : ObservableObject
    {

      
        private readonly ControllerSkidshopView skidshopController;
       
        private ObservableCollection<Utrustning> valdaUtrustningar = null!;
        public ObservableCollection<Utrustning> ValdaUtrustningar { get => valdaUtrustningar; set { valdaUtrustningar = value; OnPropertyChanged(); } }
        private ObservableCollection<Utrustning> tillgängligaUtrustningar = null!;
        public ObservableCollection<Utrustning> TillgängligaUtrustningar { get => tillgängligaUtrustningar; set { tillgängligaUtrustningar = value; OnPropertyChanged(); } }

        public ObservableCollection<Kund> valdaKunder = null!;
        public ObservableCollection<Kund> ValdaKunder { get => valdaKunder; set { valdaKunder = value; OnPropertyChanged(); } }

        private ObservableCollection<Skidskola> valdaLektioner = null!;
        public ObservableCollection<Skidskola> ValdaLektioner { get => valdaLektioner; set { valdaLektioner = value; OnPropertyChanged(); } }
        private ObservableCollection<Skidskola> tillgängligaLektioner = null!;
        public ObservableCollection<Skidskola> TillgängligaLektioner { get => tillgängligaLektioner; set { tillgängligaLektioner = value; OnPropertyChanged(); } }

        public ObservableCollection<SkidShopView> skidshopbokningar = null!;
        public ObservableCollection<SkidShopView> SkidshopBokningar { get => skidshopbokningar; set { skidshopbokningar = value; OnPropertyChanged(); } }

        
        public ObservableCollection<SkidShopView> aktivaSkidshopBokningar = null!;
        public ObservableCollection<SkidShopView> AktivaSkidShopBokningar { get => aktivaSkidshopBokningar; set { aktivaSkidshopBokningar = value; OnPropertyChanged(); } }

        private void Refresh()
        {
            ValdaUtrustningar.Clear();
            ValdaLektioner.Clear();
           
        }


        private Skidskola lektionerSelectedItem = null!;
        public Skidskola LektionerSelectedItem
        {
            get { return lektionerSelectedItem; }
            set { lektionerSelectedItem = value; OnPropertyChanged(); }
        }

        private Skidskola tillgängligaLektionerSelectedItem = null!;
        public Skidskola TillgängligaLektionerSelectedItem
        {
            get { return tillgängligaLektionerSelectedItem; }
            set
            {
                tillgängligaLektionerSelectedItem = value;
                OnPropertyChanged();
            }
        }
        private int lektionerSelectedIndex;
        public int LektionerSelectedIndex
        {
            get { return lektionerSelectedIndex; }
            set { lektionerSelectedIndex = value; OnPropertyChanged(); }
        }
        private int tillgängligaKunderSelectedIndex;
        public int TillgängligaKunderSelectedIndex { get { return tillgängligaKunderSelectedIndex; } set { tillgängligaKunderSelectedIndex = value; OnPropertyChanged(); } }


        private Utrustning utrustningarSelectedItem = null!;
        public Utrustning UtrustningarSelectedItem
        {
            get { return utrustningarSelectedItem; }
            set { utrustningarSelectedItem = value; OnPropertyChanged(); }

        }

        private Utrustning tillgängligaUtrustningarSelectedItem = null!;
        public Utrustning TillgängligaUtrustningarSelectedItem
        {
            get { return tillgängligaUtrustningarSelectedItem; }
            set
            {
                tillgängligaUtrustningarSelectedItem = value;
                OnPropertyChanged();
            }
        }
        private int utrustningarSelectedIndex;
        public int UtrustningarSelectedIndex
        {
            get { return utrustningarSelectedIndex; }
            set { utrustningarSelectedIndex = value; OnPropertyChanged(); }
        }


        private Kund kunderSelectedItem = null!;
        public Kund KunderSelectedItem
        {
            get { return kunderSelectedItem; }
            set { kunderSelectedItem = value; OnPropertyChanged(); }
        }
        private Kund tillgängligaKunderSelectedItem = null!;
        public Kund TillgängligaKunderSelectedItem
        {
            get { return tillgängligaKunderSelectedItem; }
            set
            {
                tillgängligaKunderSelectedItem = value;
                OnPropertyChanged();
            }
        }


        private bool isNotModified = true;
        public bool IsNotModified
        {
            get { return isNotModified; }
            set { isNotModified = value; OnPropertyChanged(); }
        }



        private SkidShopView bokningSelectedItem = null!;
        public SkidShopView BokningSelectedItem
        {
            get { return bokningSelectedItem; }
            set { bokningSelectedItem = value; OnPropertyChanged(); }
        }


        public SkidshopViewModel()
        {
           
            skidshopController = new ControllerSkidshopView();
            TillgängligaUtrustningar = new ObservableCollection<Utrustning>();
            SkidshopBokningar = new ObservableCollection<SkidShopView>();
            ValdaUtrustningar = new ObservableCollection<Utrustning>();
            ValdaKunder = new ObservableCollection<Kund>();
            ValdaKunder = new ObservableCollection<Kund>();
            TillgängligaUtrustningar = new ObservableCollection<Utrustning>();
            TillgängligaLektioner = new ObservableCollection<Skidskola>();
            ValdaLektioner = new ObservableCollection<Skidskola>();
            ReadSkidShopBokningarCommand.Execute(null!);
            ReadUtrustningCommand.Execute(null!);
            ReadLektionerCommand.Execute(null!);
        }

        private string selectedLektion;
        public string SelectedLektion
        {
            get { return selectedLektion; }
            set
            {
                if (selectedLektion != value)
                {
                    selectedLektion = value; OnPropertyChanged(); Generator1();
                }
            }
        }
        private string selectedUtrustning;
        public string SelectedUtrustning
        {
            get { return selectedUtrustning; }
            set
            {
                if (selectedUtrustning != value)
                {
                    selectedUtrustning = value; OnPropertyChanged(); Generator();
                }
            }
        }
        private string selectedDag;
        public string SelectedDag
        {
            get { return selectedDag; }
            set
            {
                if (selectedDag != value)
                {
                    selectedDag = value; OnPropertyChanged();
                }
            }
        }

        private string _selectedWeek;
        public string SelectedWeek
        {
            get { return _selectedWeek; }
            set
            {
                if (_selectedWeek != value)
                {
                    _selectedWeek = value; OnPropertyChanged();
                }
            }
        }

        private int? totalaPriset = 0;

        public int? TotalaPriset
        {
            get { return totalaPriset; }
            set { totalaPriset = value; OnPropertyChanged(); }
        }

        private string kundNr;
        public string Kundnummer
        {
            get { return kundNr; }
            set
            {
                kundNr = value;
                OnPropertyChanged();
            }
        }

        private DateTime valtDatum = DateTime.Now;
        public DateTime ValtDatum
        {
            get { return valtDatum; }
            set
            {
                valtDatum = value;
                OnPropertyChanged(nameof(ValtDatum));
            }
        }

        private int? antalDagar;
        public int? AntalDagar
        {
            get { return antalDagar; }
            set { antalDagar = value; OnPropertyChanged(); }
        }

        private int? skidshopbokningsID;
        public int? SkidshopBokningsID
        {
            get { return skidshopbokningsID; }
            set { skidshopbokningsID = value; OnPropertyChanged(); }
        }

        private bool bookingByWeek;
        public bool BookingByWeek
        {
            get { return bookingByWeek; }
            set
            {
                if (bookingByWeek != value)
                {
                    bookingByWeek = value;
                    OnPropertyChanged(nameof(BookingByWeek));
                }
            }
        }
        //ReadLogialerCommand 

        private ICommand readLektionerCommand = null!;
        public ICommand ReadLektionerCommand => readLektionerCommand ??= readLektionerCommand = new RelayCommand(() =>
        {
            TillgängligaLektioner = new ObservableCollection<Skidskola>(skidshopController.HämtaLektioner());
        });

        private ICommand readUtrustningCommand = null!;
        public ICommand ReadUtrustningCommand => readUtrustningCommand ??= readUtrustningCommand = new RelayCommand(() =>
        {
            TillgängligaUtrustningar = new ObservableCollection<Utrustning>(skidshopController.HämtaUtrustningar());
        });


        private ICommand readSkidShopBoknigarCommand = null!;
        public ICommand ReadSkidShopBokningarCommand => readSkidShopBoknigarCommand ??= readSkidShopBoknigarCommand = new RelayCommand(() =>
        {

            SkidshopBokningar = new ObservableCollection<SkidShopView>(skidshopController.HämtaSkidshopBokningar());
        });

        //Addcommand 
        private ICommand addUtrustningCommand = null!;
        public ICommand AddUtrustningCommand => addUtrustningCommand ??= new RelayCommand(() =>
        {
            if (UtrustningarSelectedItem != null && TillgängligaUtrustningar.Contains(UtrustningarSelectedItem))
            {
                ValdaUtrustningar.Add(UtrustningarSelectedItem);
                TillgängligaUtrustningar.Remove(UtrustningarSelectedItem);

                IsNotModified = false;

            }
        }, () => UtrustningarSelectedItem != null && TillgängligaUtrustningar.Contains(UtrustningarSelectedItem));


        private ICommand addLektionerCommand = null!;
        public ICommand AddLektionerCommand => addLektionerCommand ??= new RelayCommand(() =>
        {
            if (LektionerSelectedItem != null && TillgängligaLektioner.Contains(LektionerSelectedItem))
            {
                ValdaLektioner.Add(LektionerSelectedItem);
                TillgängligaLektioner.Remove(lektionerSelectedItem);

                IsNotModified = false;
            }
        }, () => LektionerSelectedItem != null && TillgängligaLektioner.Contains(LektionerSelectedItem));


        //Refresh command 
        private ICommand refreshCommand = null!;
        public ICommand RefreshCommand => refreshCommand ??= refreshCommand = new RelayCommand(() =>
        {
            TillgängligaLektioner = new ObservableCollection<Skidskola>(skidshopController.HämtaLektioner());

        });



        //// Remove Commands 
        private ICommand tabortUtrustningCommand = null!;
        public ICommand TabortUtrustningCommand => tabortUtrustningCommand ??= new RelayCommand(() =>
        {
            if (UtrustningarSelectedItem != null && ValdaUtrustningar.Contains(UtrustningarSelectedItem))
            {
                TillgängligaUtrustningar.Add(UtrustningarSelectedItem);
                ValdaUtrustningar.Remove(UtrustningarSelectedItem);

                IsNotModified = false;
            }
        }, () => UtrustningarSelectedItem != null && ValdaUtrustningar.Contains(UtrustningarSelectedItem));


        private ICommand tabortLektionerCommand = null!;
        public ICommand TabortLektionerCommand => tabortLektionerCommand ??= new RelayCommand(() =>
        {
            if (LektionerSelectedItem != null && ValdaLektioner.Contains(LektionerSelectedItem))
            {
                TillgängligaLektioner.Add(LektionerSelectedItem);
                ValdaLektioner.Remove(LektionerSelectedItem);

                IsNotModified = false;
            }
        }, () => LektionerSelectedItem != null && ValdaLektioner.Contains(LektionerSelectedItem));

        //Skapa BokningCommand

        private int nuvarandeVecka;
        private RelayCommand skapaSkidshopBokningCommand = null!;
        public RelayCommand SkapaSkidShopBokningCommand => skapaSkidshopBokningCommand ??= new RelayCommand(async () =>
        {
            try
            {
                // 1. Start Transaction (pseudo-code, adapt to your actual API)
                await skidshopController.BeginTransaction();

                int? totalpris = TotalaPriset;
                int kundNr = int.Parse(Kundnummer);
                DateTime startTid = ValtDatum;
                int? antalDagar = AntalDagar;
                DateTime slutTid = startTid.AddDays(antalDagar ?? 0);

                List<Utrustning> valdaUtrustningarList = ValdaUtrustningar?.ToList() ?? new List<Utrustning>();
                List<Skidskola> valdaLektionerList = ValdaLektioner?.ToList() ?? new List<Skidskola>();

                if (BookingByWeek)
                {
                    if (SelectedWeek != null)
                    {
                        // Assuming SelectedWeek is a string representing the week number
                        // And you have a method to convert week number to DateTime range
                        (startTid, slutTid) = skidshopController.ConvertWeekToDateTimeRange(SelectedWeek);
                    }
                    else
                    {
                        MessageBox.Show("Please select a week for your booking.", "Booking Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                SkidShopView b = await skidshopController.CreateSkidShopBokingAsync(kundNr, startTid, slutTid, valdaUtrustningarList, valdaLektionerList, totalpris);

                await skidshopController.CommitTransaction();

                MessageBox.Show($"Bokningen är bekräftad:\n\nKundnamn: {b.KundNr.namn}\nStarttid: {startTid}\nAntal dagar: {antalDagar}\nSluttid: {slutTid}\nUtrustning: {string.Join(", ", valdaUtrustningarList.Select(u => u.ToString()))}\nLektioner: {string.Join(", ", valdaLektionerList.Select(l => l.ToString()))}");

                ValdaLektioner.Clear();
                ValdaUtrustningar.Clear();
                TotalaPriset = 0;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Booking Error", MessageBoxButton.OK, MessageBoxImage.Error);

               
                await skidshopController.RollbackTransaction();
            }
           
        }, () => !string.IsNullOrEmpty(Kundnummer));


        //Listor för Lektioner

        public List<string> Lektioner { get; } = new List<string>
{
    "Röd", "Blå", "Svart", "Grön", "Privat"
};

        //Listor för dag
        public List<string> Dag { get; } = new List<string>
{
    "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag"
};


        //selectLektion 
        public void Generator1()
        {

            if (SelectedLektion == "Röd")
            {
                VisaTillgängligaLektionerRödCommand.Execute(null!);

            }
            else if (SelectedLektion == "Grön")
            {
                VisaTillgängligaLektionerGrönCommand.Execute(null!);
            }
            else if (SelectedLektion == "Blå")
            {
                VisaTillgängligaLektionerBlåCommand.Execute(null!);
            }
            else if (SelectedLektion == "Svart")
            {

                VisaTillgängligaLektionerSvartCommand.Execute(null!);
            }
            else if (SelectedLektion == "Privat")
            {
                VisaTillgängligaLektionerPrivatLektionCommand.Execute(null!);
            }

        }

        //Skidskola Commands

        int senasteValdLektionIndex = -1;
        private RelayCommand visaTillgängligaLektionerRödCommand = null!;
        public RelayCommand VisaTillgängligaLektionerRödCommand => visaTillgängligaLektionerRödCommand ??= new RelayCommand(() =>
        {
            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> tillgängligaLektioner = skidshopController.HämtaLektioner()
       .Where(lektion =>
           lektion.Typ == LektionTyp.Röd &&
           !skidshopController.HämtaBokningsRaderLektionSkidshop()
               .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senasteValdLektionIndex++;
            if (senasteValdLektionIndex < tillgängligaLektioner.Count)
            {
                ValdaLektioner.Add(tillgängligaLektioner[senasteValdLektionIndex]);
            }
            else
            {

                senasteValdLektionIndex = -1;
                MessageBox.Show("Alla lektioner har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola valdUtrustning = tillgängligaLektioner.FirstOrDefault();
            if (!string.IsNullOrEmpty(SelectedDag))
            {

                if (SelectedLektion == "Röd" && SelectedDag == "Måndag" || SelectedDag == "Tisdag" || SelectedDag == "Onsdag")
                {
                    TotalaPriset += 425;
                }
                else if (SelectedLektion == "Röd" && SelectedDag == "Torsdag" || SelectedDag == "Fredag")
                {
                    TotalaPriset += 525;
                }
            }
        });
        int senastValdLektionGrönIndex = -1;
        private RelayCommand visaTillgängligaLektionerGrönCommand = null!;
        public RelayCommand VisaTillgängligaLektionerGrönCommand => visaTillgängligaLektionerGrönCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> tillgängligaLektioner = skidshopController.HämtaLektioner()
         .Where(lektion =>
             lektion.Typ == LektionTyp.Grön &&
             !skidshopController.HämtaBokningsRaderLektionSkidshop()
                 .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senastValdLektionGrönIndex++;
            if (senastValdLektionGrönIndex < tillgängligaLektioner.Count)
            {
                ValdaLektioner.Add(tillgängligaLektioner[senastValdLektionGrönIndex]);
            }
            else
            {

                senastValdLektionGrönIndex = -1;
                MessageBox.Show("Alla lektioner har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola valdUtrustning = tillgängligaLektioner.FirstOrDefault();
            if (!string.IsNullOrEmpty(SelectedDag))
            {

                if (SelectedLektion == "Grön" && SelectedDag == "Måndag" || SelectedDag == "Tisdag" || SelectedDag == "Onsdag")
                {
                    TotalaPriset += 400;
                }
                else if (SelectedLektion == "Grön" && SelectedDag == "Torsdag" || SelectedDag == "Fredag")
                {
                    TotalaPriset += 500;
                }
            }
        });

        int senasteValdLektionBlåIndex = -1;
        private RelayCommand visaTillgängligaLektionerBlåCommand = null!;
        public RelayCommand VisaTillgängligaLektionerBlåCommand => visaTillgängligaLektionerBlåCommand ??= new RelayCommand(() =>
        {
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> tillgängligaLektioner = skidshopController.HämtaLektioner()
         .Where(lektion =>
             lektion.Typ == LektionTyp.Blå &&
             !skidshopController.HämtaBokningsRaderLektionSkidshop()
                 .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senasteValdLektionBlåIndex++;
            if (senasteValdLektionBlåIndex < tillgängligaLektioner.Count)
            {
                ValdaLektioner.Add(tillgängligaLektioner[senasteValdLektionBlåIndex]);
            }
            else
            {

                senasteValdLektionBlåIndex = -1;
                MessageBox.Show("Alla lektioner har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola valdUtrustning = tillgängligaLektioner.FirstOrDefault();
            if (!string.IsNullOrEmpty(SelectedDag))
            {

                if (SelectedLektion == "Blå" && SelectedDag == "Måndag" || SelectedDag == "Tisdag" || SelectedDag == "Onsdag")
                {
                    TotalaPriset += 415;
                }
                else if (SelectedLektion == "Blå" && SelectedDag == "Torsdag" || SelectedDag == "Fredag")
                {
                    TotalaPriset += 515;
                }
            }
        });

        int senasteValdLektionSvartIndex = -1;
        private RelayCommand visaTillgängligaLektionerSvartCommand = null!;
        public RelayCommand VisaTillgängligaLektionerSvartCommand => visaTillgängligaLektionerSvartCommand ??= new RelayCommand(() =>
        {
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> tillgängligaLektioner = skidshopController.HämtaLektioner()
         .Where(lektion =>
             lektion.Typ == LektionTyp.Svart &&
             !skidshopController.HämtaBokningsRaderLektionSkidshop()
                 .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senasteValdLektionSvartIndex++;
            if (senasteValdLektionSvartIndex < tillgängligaLektioner.Count)
            {
                ValdaLektioner.Add(tillgängligaLektioner[senasteValdLektionSvartIndex]);
            }
            else
            {

                senasteValdLektionSvartIndex = -1;
                MessageBox.Show("Alla lektioner har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola valdUtrustning = tillgängligaLektioner.FirstOrDefault();
            if (!string.IsNullOrEmpty(SelectedDag))
            {

                if (SelectedLektion == "Svart" && SelectedDag == "Måndag" || SelectedDag == "Tisdag" || SelectedDag == "Onsdag")
                {
                    TotalaPriset += 455;
                }
                else if (SelectedLektion == "Svart" && SelectedDag == "Torsdag" || SelectedDag == "Fredag")
                {
                    TotalaPriset += 555;
                }
            }
        });

        int senastValdPrivatLektionIndex = -1;
        private RelayCommand visaTillgängligaLektionerPrivatLektionCommand = null!;
        public RelayCommand VisaTillgängligaLektionerPrivatLektionCommand => visaTillgängligaLektionerPrivatLektionCommand ??= new RelayCommand(() =>
        {
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> tillgängligaLektioner = skidshopController.HämtaLektioner()
          .Where(lektion =>
              lektion.Typ == LektionTyp.PrivatLektion &&
              !skidshopController.HämtaBokningsRaderLektionSkidshop()
                  .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                  .Any(bokningsRad =>
                      (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
          .ToList();


            senastValdPrivatLektionIndex++;
            if (senastValdPrivatLektionIndex < tillgängligaLektioner.Count)
            {
                ValdaLektioner.Add(tillgängligaLektioner[senastValdPrivatLektionIndex]);
            }
            else
            {

                senastValdPrivatLektionIndex = -1;
                MessageBox.Show("Alla lektioner har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola valdUtrustning = tillgängligaLektioner.FirstOrDefault();
            if (!string.IsNullOrEmpty(SelectedDag))
            {


                TotalaPriset += 375;


            }
        });

        //List Uturstningstyp
        public List<string> Utrustningar { get; } = new List<string>
{
    "AlpintPaket", "AlpintSkidor", "AlpintPjäxor", "AlpintStavar", "LängdSkidor", "LängdPjäxor", "LängdStavar", "LängdPaket", "PaketSnowbord", "SnowbordSkor",
    "Snowbord", "Hjälm", "Lynx 50", "Yamaha Viking", "Pulka",

};

        //SelectUturstning 
        public void Generator()
        {

            if (selectedUtrustning == "AlpintPaket")
            {
                VisaTillgängligaUtrustningarPaketAlpintCommand.Execute(null!);

            }
            else if (selectedUtrustning == "AlpintSkidor")
            {
                VisaTillgängligaUtrustningarSkidorAlpintCommand.Execute(null!);
            }
            else if (selectedUtrustning == "AlpintPjäxor")
            {
                VisaTillgängligaUtrustningarPjäxorAlpintCommand.Execute(null!);
            }
            else if (selectedUtrustning == "AlpintStavar")
            {

                VisaTillgängligaUtrustningarStavarAlpintCommand.Execute(null!);
            }
            else if (selectedUtrustning == "LängdPaket")
            {
                VisaTillgängligaUtrustningarPaketLängdCommand.Execute(null!);
            }
            else if (selectedUtrustning == "LängdSkidor")
            {
                VisaTillgängligaUtrustningarLängdSkidorCommand.Execute(null!);
            }
            else if (selectedUtrustning == "LängdPjäxor")
            {
                VisaTillgängligaUtrustningarSkidPjäxorLängdCommand.Execute(null!);
            }
            else if (selectedUtrustning == "LängdStavar")
            {
                VisaTillgängligaUtrustningarStavarLängdCommand.Execute(null!);
            }
            else if (selectedUtrustning == "PaketSnowbord")
            {
                VisaTillgängligaUtrustningarPaketSnowbordCommand.Execute(null!);
            }
            else if (selectedUtrustning == "Snowbord")
            {
                VisaTillgängligaUtrustningarSnowboardCommand.Execute(null!);
            }
            else if (selectedUtrustning == "SnowbordSkor")
            {
                VisaTillgängligaUtrustningarSnowboardSkorCommand.Execute(null!);
            }
            else if (selectedUtrustning == "Hjälm")
            {
                VisaTillgängligaUtrustningarHjälmCommand.Execute(null!);
            }
            else if (selectedUtrustning == "Lynx 50")
            {
                VisaTillgängligaUtrustningarLynx50Command.Execute(null!);
            }
            else if (selectedUtrustning == "Yamaha Viking")
            {
                VisaTillgängligaUtrustningarYamahaVikingCommand.Execute(null!);
            }
            else if (selectedUtrustning == "Pulka")
            {
                VisaTillgängligaUtrustningarPulkaCommand.Execute(null!);

            }
        }

        //Utrustning commands

        int senasteValdPaketAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarPaketAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarPaketAlpintCommand => visaTillgängligaUtrustningarPaketAlpintCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.PaketAlpint &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();

            senasteValdPaketAlpintIndex++;
            if (senasteValdPaketAlpintIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senasteValdPaketAlpintIndex]);
            }
            else
            {

                senasteValdPaketAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {


                if (antalDagar == 1)
                {
                    TotalaPriset += 180;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 305;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 405;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 495;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 560;
                }
            }
        });


        int senasteValdSkidorAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarSkidorAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarSkidorAlpintCommand => visaTillgängligaUtrustningarSkidorAlpintCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totaltBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrustningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.SkidorAlpint &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senasteValdSkidorAlpintIndex++;
            if (senasteValdSkidorAlpintIndex < tillgängligaUtrustningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrustningar[senasteValdSkidorAlpintIndex]);
            }
            else
            {

                senasteValdSkidorAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrustningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 130;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 230;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 330;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 395;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 445;
                }
            }
        });

        int senasteValdPjäxAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarPjäxorAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarPjäxorAlpintCommand => visaTillgängligaUtrustningarPjäxorAlpintCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.PjäxorAlpint &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();

            senasteValdPjäxAlpintIndex++;
            if (senasteValdPjäxAlpintIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senasteValdPjäxAlpintIndex]);
            }
            else
            {

                senasteValdPjäxAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 115;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 195;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 255;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 315;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 375;
                }
            }
        });


        int senasteValdUtrustningStavarAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarStavarAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarStavarAlpintCommand => visaTillgängligaUtrustningarStavarAlpintCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.StavarAlpint &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();

            senasteValdUtrustningStavarAlpintIndex++;
            if (senasteValdUtrustningStavarAlpintIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senasteValdUtrustningStavarAlpintIndex]);
            }
            else
            {

                senasteValdUtrustningStavarAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 40;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 50;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 60;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 70;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 80;
                }
            }
        });

        int senasteValdUtrustningPaketLängdIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarPaketLängdCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarPaketLängdCommand => visaTillgängligaUtrustningarPaketLängdCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.PaketLängd &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();

            senasteValdUtrustningPaketLängdIndex++;
            if (senasteValdUtrustningPaketLängdIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senasteValdUtrustningPaketLängdIndex]);
            }
            else
            {

                senasteValdUtrustningPaketLängdIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 130;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 230;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 320;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 390;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 440;
                }
            }
        });

        int senastValdUtrusningLängdSkidorIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarLängSkidorCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarLängdSkidorCommand => visaTillgängligaUtrustningarLängSkidorCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.LängdSkidor &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senastValdUtrusningLängdSkidorIndex++;
            if (senastValdUtrusningLängdSkidorIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senastValdUtrusningLängdSkidorIndex]);
            }
            else
            {

                senastValdUtrusningLängdSkidorIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 100;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 170;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 220;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 270;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 320;
                }
            }
        });

        int senastValdutrustningskidpjäxorIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarSkidPjäxorLängdCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarSkidPjäxorLängdCommand => visaTillgängligaUtrustningarSkidPjäxorLängdCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.SkidPjäxorLängd &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senastValdutrustningskidpjäxorIndex++;
            if (senastValdutrustningskidpjäxorIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senastValdutrustningskidpjäxorIndex]);
            }
            else
            {

                senastValdutrustningskidpjäxorIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 80;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 120;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 150;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 170;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 200;
                }
            }
        });

        int senastValdutrustStavarIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarStavarLängdCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarStavarLängdCommand => visaTillgängligaUtrustningarStavarLängdCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
         .Where(utrustning =>
             utrustning.Typ == UtrustningTyp.StavarLängd &&
             !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                 .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senastValdutrustStavarIndex++;
            if (senastValdutrustStavarIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senastValdutrustStavarIndex]);
            }
            else
            {

                senastValdutrustStavarIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 40;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 50;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 60;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 70;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 80;
                }
            }
        });

        int senasteValdutrustPaketSnowbordIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarPaketSnowbordCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarPaketSnowbordCommand => visaTillgängligaUtrustningarPaketSnowbordCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.PaketSnowbord &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senasteValdutrustPaketSnowbordIndex++;
            if (senasteValdutrustPaketSnowbordIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senasteValdutrustPaketSnowbordIndex]);
            }
            else
            {

                senasteValdutrustPaketSnowbordIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 250;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 415;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 540;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 655;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 760;
                }
            }
        });
        int senastValdutrustningSnowbordIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarSnowboardCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarSnowboardCommand => visaTillgängligaUtrustningarSnowboardCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
         .Where(utrustning =>
             utrustning.Typ == UtrustningTyp.Snowboard &&
             !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                 .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();

            senastValdutrustningSnowbordIndex++;
            if (senastValdutrustningSnowbordIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senastValdutrustningSnowbordIndex]);
            }
            else
            {

                senastValdutrustningSnowbordIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.PaketSnowbord;

                if (antalDagar == 1)
                {
                    TotalaPriset += 190;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 335;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 455;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 555;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 625;
                }
            }
        });

        int senastValdUtrustningSnowboardSkorIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarSnowboardSkorCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarSnowboardSkorCommand => visaTillgängligaUtrustningarSnowboardSkorCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.SnowboardSkor &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();

            senastValdUtrustningSnowboardSkorIndex++;
            if (senastValdUtrustningSnowboardSkorIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senastValdUtrustningSnowboardSkorIndex]);
            }
            else
            {

                senastValdUtrustningSnowboardSkorIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 115;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 195;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 275;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 350;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 395;
                }
            }
        });


        int senastValdHjälmIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarHjälmCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarHjälmCommand => visaTillgängligaUtrustningarHjälmCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.Hjälm &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();

            senastValdHjälmIndex++;
            if (senastValdHjälmIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senastValdHjälmIndex]);
            }
            else
            {

                senastValdHjälmIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {


                if (antalDagar == 1)
                {
                    TotalaPriset += 40;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 50;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 60;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 70;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 80;
                }
            }
        });

        int senastValdLynx50Index = -1;
        private RelayCommand visaTillgängligaUtrustningarLynx50Command = null!;

        public RelayCommand VisaTillgängligaUtrustningarLynx50Command => visaTillgängligaUtrustningarLynx50Command ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.Lynx50 &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senastValdLynx50Index++;
            if (senastValdLynx50Index < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senastValdLynx50Index]);
            }
            else
            {

                senastValdLynx50Index = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {


                if (antalDagar == 1)
                {
                    TotalaPriset += 1000;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 1700;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 2750;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 4000;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 5950;
                }
            }
        });

        int senasteValdYamahaVikingIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarYamahaVikingCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarYamahaVikingCommand => visaTillgängligaUtrustningarYamahaVikingCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.YamahaViking &&
            !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senasteValdYamahaVikingIndex++;
            if (senasteValdYamahaVikingIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senasteValdYamahaVikingIndex]);
            }
            else
            {

                senasteValdYamahaVikingIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.AlpintPaket;

                if (antalDagar == 1)
                {
                    TotalaPriset += 1300;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 2000;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 3700;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 5000;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 7250;
                }
            }
        });

        int senasteValdaPulkaIndex = -1;
        private RelayCommand visaTillgängligaUtrustningarPulkaCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningarPulkaCommand => visaTillgängligaUtrustningarPulkaCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> tillgängligaUtrusningar = skidshopController.HämtaUtrustningar()
          .Where(utrustning =>
              utrustning.Typ == UtrustningTyp.Pulka &&
              !skidshopController.HämtaBokningsRaderUtrustningSkidshop()
                  .Where(bokningsRad => bokningsRad.UtrusningsID == utrustning.utrustningID)
                  .Any(bokningsRad =>
                      (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
          .ToList();


            senasteValdaPulkaIndex++;
            if (senasteValdaPulkaIndex < tillgängligaUtrusningar.Count)
            {
                ValdaUtrustningar.Add(tillgängligaUtrusningar[senasteValdaPulkaIndex]);
            }
            else
            {

                senasteValdaPulkaIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning valdUtrustning = tillgängligaUtrusningar.FirstOrDefault();
            if (valdUtrustning != null)
            {
                Dictionary<int, int> priceDictionary = valdUtrustning.NilaPulka;

                if (antalDagar == 1)
                {
                    TotalaPriset += 240;
                }
                else if (antalDagar == 2)
                {
                    TotalaPriset += 415;
                }
                else if (antalDagar == 3)
                {
                    TotalaPriset += 640;
                }
                else if (antalDagar == 4)
                {
                    TotalaPriset += 800;
                }
                else if (antalDagar == 5)
                {
                    TotalaPriset += 1280;
                }
            }
        });



       
        //Pris kalkyl command
        private int CalculatePriceForWeekRange(string weekRange, Dictionary<string, int> priceDictionary)
        {
            if (priceDictionary.TryGetValue(weekRange, out int price))
            {
                return price;
            }

            // Parse week range (e.g., "14-22")
            var parts = weekRange.Split('-');
            if (parts.Length == 2 && int.TryParse(parts[0], out int startWeek) && int.TryParse(parts[1], out int endWeek))
            {
                for (int week = startWeek; week <= endWeek; week++)
                {
                    if (priceDictionary.TryGetValue(week.ToString(), out price))
                    {
                        return price;
                    }
                }
            }

            // If no price found, handle as an error or return a default value
            MessageBox.Show($"No price found for week range: {weekRange}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return 0;
        }

        //Lista av veckorna
        public List<string> Veckor { get; } = new List<string>
{
    "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
    "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
    "21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
    "31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
    "41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
    "51", "52"
};

        //SökBokningCommand Command

        private ICommand sökSkidshopBokningCommand = null;

        public ICommand SökSkidshopBokningCommand
        {
            get
            {
                if (sökSkidshopBokningCommand == null)
                {
                    sökSkidshopBokningCommand = new RelayCommand(() =>
                    {
                        if (skidshopbokningsID.HasValue && int.TryParse(skidshopbokningsID.ToString(), out int parsedSkidshopbookingsID))
                        {
                            var hittadSkidshopBokning = skidshopController?.SökSkidshopBokning(parsedSkidshopbookingsID);
                            if (hittadSkidshopBokning != null)
                            {
                                SkidshopBokningar?.Clear();
                                SkidshopBokningar?.Add(hittadSkidshopBokning);
                                ValdaUtrustningar?.Clear();

                               
                            }
                        }
                        else
                        {
                            SkidshopBokningar = new ObservableCollection<SkidShopView>(skidshopController?.HämtaSkidshopBokningar() ?? new List<SkidShopView>());
                            // Optional error message:
                            // MessageBox.Show("Invalid booking ID", "Error");
                        }
                    });

                }

                return sökSkidshopBokningCommand;
            }
        }

        //skravut register commands

        private RelayCommand skrivaUtKundRegister = null!;
        public RelayCommand SkrivaUtKundRegister => skrivaUtKundRegister ??= new RelayCommand(() =>
        {
            var kundLista = skidshopController.HämtaKunder(); // Assuming this method returns a list of customers

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run("Booking Confirmation")));
                paragraph.Inlines.Add(new LineBreak());

                foreach (var kund in kundLista)
                {
                    paragraph.Inlines.Add(new Run("Kund Namn: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + kund.namn));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Kund Nummer: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + kund.kundID));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Kredit Gräns: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + kund.kreditGräns));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Rabatt: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + kund.rabatt));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new LineBreak()); // Adds an extra line break between customers
                }

                document.Blocks.Add(paragraph);
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Booking Confirmation");
            }
        });

        private RelayCommand skrivaUtSkidutrusningsregistret = null!;
        public RelayCommand SkrivaUtSkidutrusningsregistret => skrivaUtSkidutrusningsregistret ??= new RelayCommand(() =>
        {
            var utrsutninglista = skidshopController.HämtaUtrustningar(); // Assuming this method returns a list of customers

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run("Booking Confirmation")));
                paragraph.Inlines.Add(new LineBreak());
             

                foreach (var ut in utrsutninglista)
                {
                    paragraph.Inlines.Add(new Run("Utrustning ID: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + ut.utrustningID));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Benämning: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + ut.benämning));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Typ: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + ut.Typ));
                    paragraph.Inlines.Add(new LineBreak());
                   
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new LineBreak()); // Adds an extra line break between customers
                }

                document.Blocks.Add(paragraph);
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Booking Confirmation");
            }
        });


        private RelayCommand skrivaUtPrislistorUtrustning = null!;
        public RelayCommand SkrivaUtPrislistorUtrustning => skrivaUtPrislistorUtrustning ??= new RelayCommand(() =>
        {
            var utrsutninglista = skidshopController.HämtaUtrustningar();

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run("Booking Confirmation")));
                paragraph.Inlines.Add(new LineBreak());

                foreach (var ut in utrsutninglista)
                {
                    paragraph.Inlines.Add(new Run("Utrustning ID: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + ut.utrustningID));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Benämning: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + ut.benämning));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Typ: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + ut.Typ));
                    paragraph.Inlines.Add(new LineBreak());

                    
                    if (ut.AlpintPaket is Dictionary<int, int> pricesData1)
                    {
                        foreach (var price in pricesData1)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.AlpintSkidor is Dictionary<int, int> pricesData2)
                    {
                        foreach (var price in pricesData2)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka {price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.AlpintPjäxor is Dictionary<int, int> pricesData3)
                    {
                        foreach (var price in pricesData3)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.AlpintStavar is Dictionary<int, int> pricesData4)
                    {
                        foreach (var price in pricesData4)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.LängdPaket is Dictionary<int, int> pricesData5)
                    {
                        foreach (var price in pricesData5)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka  {price.Key}: Pris  {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.LängdSkidor is Dictionary<int, int> pricesData6)
                    {
                        foreach (var price in pricesData6)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka {price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.LängdPjäxor is Dictionary<int, int> pricesData7)
                    {
                        foreach (var price in pricesData7)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.AlpintStavar is Dictionary<int, int> pricesData8)
                    {
                        foreach (var price in pricesData8)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.PaketSnowbord is Dictionary<int, int> pricesData9)
                    {
                        foreach (var price in pricesData9)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.Snowbord is Dictionary<int, int> pricesData10)
                    {
                        foreach (var price in pricesData10)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.SkorSnowbord is Dictionary<int, int> pricesData11)
                    {
                        foreach (var price in pricesData11)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.Hjälm is Dictionary<int, int> pricesData12)
                    {
                        foreach (var price in pricesData12)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.SkoterLynx50 is Dictionary<int, int> pricesData13)
                    {
                        foreach (var price in pricesData13)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}: Pris{price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.YamahaViking is Dictionary<int, int> pricesData14)
                    {
                        foreach (var price in pricesData14)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}:Pris {price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }
                    if (ut.NilaPulka is Dictionary<int, int> pricesData15)
                    {
                        foreach (var price in pricesData15)
                        {
                            paragraph.Inlines.Add(new Run($"Vecka{price.Key}: Pris{price.Value}"));
                            paragraph.Inlines.Add(new LineBreak());
                        }
                    }


                    paragraph.Inlines.Add(new LineBreak()); // Adds an extra line break between items
                }

                document.Blocks.Add(paragraph);
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Booking Confirmation");
            }
        });



    }
}

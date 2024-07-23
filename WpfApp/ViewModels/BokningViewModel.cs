using AffärsLager;
using DataLager;
using Entiteterna;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp.Commands;
using WpfApp.Models;
using static Entiteterna.Logial;
using static Entiteterna.Skidskola;
using static Entiteterna.Utrustning;

namespace WpfApp.ViewModels
{
    public class BokningViewModel : ObservableObject
    {
        private readonly ControllerMottagare mottagareController;



        private ObservableCollection<Logial> tillgängligaLogialer = null!;
        public ObservableCollection<Logial> TillgängligaLogialer { get => tillgängligaLogialer; set { tillgängligaLogialer = value; OnPropertyChanged(); } }

        public ObservableCollection<Logial> valdaLogialer = null!;
        public ObservableCollection<Logial> ValdaLogialer { get => valdaLogialer; set { valdaLogialer = value; OnPropertyChanged(); } }


        public ObservableCollection<TellefonMottagareView> bokningar = null!;
        public ObservableCollection<TellefonMottagareView> Bokningar { get => bokningar; set { bokningar = value; OnPropertyChanged(); } }

        public ObservableCollection<TellefonMottagareView> aktivaBokningar = null!;
        public ObservableCollection<TellefonMottagareView> AktivaBokningar { get => aktivaBokningar; set { aktivaBokningar = value; OnPropertyChanged(); } }


        public ObservableCollection<Kund> valdaKunder = null!;
        public ObservableCollection<Kund> ValdaKunder { get => valdaKunder; set { valdaKunder = value; OnPropertyChanged(); } }

        private ObservableCollection<Kund> tillgängligaKunder = new ObservableCollection<Kund>();
        public ObservableCollection<Kund> TillgängligaKunder { get => tillgängligaKunder; set { tillgängligaKunder = value; OnPropertyChanged(); } }
        private ObservableCollection<Utrustning> valdaUtrustningar = null!;
        public ObservableCollection<Utrustning> ValdaUtrustningar { get => valdaUtrustningar; set { valdaUtrustningar = value; OnPropertyChanged(); } }
        private ObservableCollection<Utrustning> tillgängligaUtrustningar = null!;
        public ObservableCollection<Utrustning> TillgängligaUtrustningar { get => tillgängligaUtrustningar; set { tillgängligaUtrustningar = value; OnPropertyChanged(); } }

        private ObservableCollection<Skidskola> valdaLektioner = null!;
        public ObservableCollection<Skidskola> ValdaLektioner { get => valdaLektioner; set { valdaLektioner = value; OnPropertyChanged(); } }
        private ObservableCollection<Skidskola> tillgängligaLektioner = null!;
        public ObservableCollection<Skidskola> TillgängligaLektioner { get => tillgängligaLektioner; set { tillgängligaLektioner = value; OnPropertyChanged(); } }

        private void Refresh()
        {
            ValdaUtrustningar.Clear();
            ValdaLektioner.Clear();
            ValdaLogialer.Clear();
        }


        private Logial logialerSelectedItem = null!;
        public Logial LogialerSelectedItem
        {
            get { return logialerSelectedItem; }
            set { logialerSelectedItem = value; OnPropertyChanged(); }
        }
        private Logial tillgängligaLogialerSelectedItem = null!;
        public Logial TillgängligaLogialerSelectedItem
        {
            get { return tillgängligaLogialerSelectedItem; }
            set
            {
                tillgängligaLogialerSelectedItem = value;
                OnPropertyChanged();
            }
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


        private int tillgängligaLogialerSelectedIndex;
        public int TillgängligaLogialerSelectedIndex { get { return tillgängligaLogialerSelectedIndex; } set { tillgängligaLogialerSelectedIndex = value; OnPropertyChanged(); } }


        private int logialerSelectedIndex;
        public int LogialerSelectedIndex
        {
            get { return logialerSelectedIndex; }
            set { logialerSelectedIndex = value; OnPropertyChanged(); }
        }

        private int tillgängligaKunderSelectedIndex;
        public int TillgängligaKunderSelectedIndex { get { return tillgängligaKunderSelectedIndex; } set { tillgängligaKunderSelectedIndex = value; OnPropertyChanged(); } }


        private int kunderSelectedIndex;
        public int KunderSelectedIndex
        {
            get { return kunderSelectedIndex; }
            set { kunderSelectedIndex = value; OnPropertyChanged(); }
        }
        private int tillgängligaUtrustningarSelectedIndex;
        public int TillgängligaUtrustningarSelectedIndex { get { return tillgängligaUtrustningarSelectedIndex; } set { tillgängligaUtrustningarSelectedIndex = value; OnPropertyChanged(); } }
        private int utrustningarSelectedIndex;
        public int UtrustningarSelectedIndex
        {
            get { return utrustningarSelectedIndex; }
            set { utrustningarSelectedIndex = value; OnPropertyChanged(); }
        }

        private int lektionerSelectedIndex;
        public int LektionerSelectedIndex
        {
            get { return lektionerSelectedIndex; }
            set { lektionerSelectedIndex = value; OnPropertyChanged(); }
        }
        private int tillgängligaLektionerSelectedIndex;
        public int TillgängligaLektionerSelectedIndex { get { return tillgängligaLektionerSelectedIndex; } set { tillgängligaLektionerSelectedIndex = value; OnPropertyChanged(); } }









        private bool isNotModified = true;
        public bool IsNotModified
        {
            get { return isNotModified; }
            set { isNotModified = value; OnPropertyChanged(); }
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

        private int växla;
        public int Växla
        {
            get { return växla; }
            set
            {
                if (växla != value)
                {
                    växla = value;
                    OnPropertyChanged(nameof(växla));
                }
            }
        }
        private RelayCommand växlaCommand = null!;
        public RelayCommand VäxlaCommand => växlaCommand ??= new RelayCommand(() =>
        {

            EfterTotalaPriset += TotalaPriset;
            FöreTotalaPriset += TotalaPriset;


        });
        private int? totalaPriset = 0;

        public int? TotalaPriset
        {
            get { return totalaPriset; }
            set
            {
                if (value > 12000)
                {
                    MessageBox.Show("Totala priset kan inte vara mer än 12000 före rabatt", "Varning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                totalaPriset = value;
                OnPropertyChanged();
            }
        }


        private int? föreTotalaPriset = 0;

        public int? FöreTotalaPriset
        {
            get { return föreTotalaPriset; }
            set
            {

                if (value != null)
                {
                    föreTotalaPriset = (int)(value * 0.8);
                }

                OnPropertyChanged();
            }
        }
        private int? efterTotalaPriset = 0;

        public int? EfterTotalaPriset
        {
            get { return efterTotalaPriset; }
            set
            {
                if (value != null)
                {
                    efterTotalaPriset = (int)(value * 0.2);
                }


                OnPropertyChanged();
            }
        }
        public void Generetor3()
        {

        }

        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                seriesCollection = value; OnPropertyChanged();

            }
        }

        private string[] logialLabels;
        public string[] LogialLabels
        {
            get { return logialLabels; }
            set
            {
                logialLabels = value; OnPropertyChanged();

            }
        }
        public DateTime[] dateLabels { get; set; }
        public DateTime[] DateLabels
        {
            get { return dateLabels; }
            set
            {
                dateLabels = value; OnPropertyChanged();

            }
        }


        private int? bokningsId;
        public int? BokningsId
        {
            get { return bokningsId; }
            set { bokningsId = value; OnPropertyChanged(); }
        }


        private TellefonMottagareView bokningSelectedItem = null!;
        public TellefonMottagareView BokningSelectedItem
        {
            get { return bokningSelectedItem; }
            set { bokningSelectedItem = value; OnPropertyChanged(); }
        }
        public int SelectedMonth
        {
            get { return selectedMonth; }
            set
            {

                selectedMonth = value; OnPropertyChanged(); SkrivaUtStatistik.Execute(null); // Update the statistics when the month changes

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

        public BokningViewModel()
        {
            mottagareController = new ControllerMottagare();
            TillgängligaLogialer = new ObservableCollection<Logial>();
            ValdaLogialer = new ObservableCollection<Logial>();
            ValdaKunder = new ObservableCollection<Kund>();
            TillgängligaKunder = new ObservableCollection<Kund>();
            ValdaUtrustningar = new ObservableCollection<Utrustning>();
            TillgängligaUtrustningar = new ObservableCollection<Utrustning>();
            TillgängligaLektioner = new ObservableCollection<Skidskola>();
            ValdaLektioner = new ObservableCollection<Skidskola>();
            Bokningar = new ObservableCollection<TellefonMottagareView>();
            ReadUtrustningCommand.Execute(null!);
            ReadKundCommand.Execute(null!);
            ReadLogialerCommand.Execute(null!);
            ReadLektionerCommand.Execute(null!);
            ReadBokningarCommand.Execute(null!);
            //SökBokningCommand.Execute(null!);

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

        // COMMANDS 

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
        private bool avbeställningsskydd;
        public bool Avbeställningsskydd
        {
            get { return avbeställningsskydd; }
            set
            {
                if (avbeställningsskydd != value)
                {
                    avbeställningsskydd = value;
                    OnPropertyChanged(nameof(avbeställningsskydd));
                }
            }
        }






        private int nuvarandeVecka;
        private RelayCommand skapaBokningCommand = null!;
        public RelayCommand SkapaBokningCommand => skapaBokningCommand ??= new RelayCommand(async () =>
        {
            try
            {

                await mottagareController.BeginTransaction();


                int? totalpris = TotalaPriset;
                int kundNr = int.Parse(Kundnummer);
                DateTime startTid = ValtDatum;
                int? antalDagar = AntalDagar;
                DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
                Kund kund = mottagareController.GetKundByID(kundNr);

                List<Logial> valdaLogialerList = ValdaLogialer.ToList();
                List<Utrustning> valdaUtrustningarList = ValdaUtrustningar.ToList();
                List<Skidskola> valdaLektionerList = ValdaLektioner.ToList();


                if (BookingByWeek)
                {
                    if (SelectedWeek != null)
                    {

                        (startTid, slutTid) = mottagareController.ConvertWeekToDateTimeRange(SelectedWeek);
                    }
                    else
                    {
                        MessageBox.Show("Please select a week for your booking.", "Booking Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                TotalaPriset *= 80 / 100;
                if (Avbeställningsskydd)
                {
                    TotalaPriset += 300;
                }



                DateTime? lastBookingDate = await mottagareController.GetLastBookingDateByKundID(kundNr);

                if (lastBookingDate.HasValue)
                {
                    int lastBookingYear = lastBookingDate.Value.Year;
                    int currentYear = DateTime.Now.Year;


                    if (currentYear == lastBookingYear + 1)
                    {

                        totalpris = (int?)(totalpris * 0.92);
                    }
                }



                TellefonMottagareView b = await mottagareController.CreateBookingAsync(kundNr, startTid, slutTid, valdaLogialerList, valdaUtrustningarList, valdaLektionerList, totalpris);

                // 3. Commit Transaction (pseudo-code)
                await mottagareController.CommitTransaction();

                MessageBox.Show($"Bokningen är bekräftad:\n\nKundnamn: {b.KundNr.namn}\nStarttid: {startTid}\nAntal dagar: {antalDagar}\nSluttid: {slutTid}\nUtrustning: {string.Join(", ", valdaUtrustningarList.Select(u => u.ToString()))}\nLektioner: {string.Join(", ", valdaLektionerList.Select(l => l.ToString()))}");

                ValdaLogialer.Clear();
                ValdaLektioner.Clear();
                ValdaUtrustningar.Clear();


                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    FlowDocument document = new FlowDocument();

                    Paragraph paragraph = new Paragraph();
                    paragraph.Inlines.Add(new Bold(new Run("Booking Confirmation")));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Kund Namn: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + kund.namn));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Kund Nummer: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + kund.kundID));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Start Time: ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + startTid));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Slut tid : ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run(" " + slutTid));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("20% av totalbeloppet som skall betalas inom 30 dagar efter bokning : ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + EfterTotalaPriset));

                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("80% av totalbeloppet som skall betalas innan bokning : ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("   " + FöreTotalaPriset));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run("Avbeställningsskydd : ") { FontWeight = FontWeights.Bold });
                    paragraph.Inlines.Add(new Run("  " + Avbeställningsskydd));
                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Bold(new Run("Logialer:")));
                    paragraph.Inlines.Add(new LineBreak());

                    foreach (var logial in valdaLogialerList)
                    {
                        paragraph.Inlines.Add(new Run(logial.ToString()));
                        paragraph.Inlines.Add(new LineBreak());
                    }

                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Bold(new Run("Utrustningar:")));
                    paragraph.Inlines.Add(new LineBreak());

                    foreach (var utrustning in valdaUtrustningarList)
                    {
                        paragraph.Inlines.Add(new Run(utrustning.ToString()));
                        paragraph.Inlines.Add(new LineBreak());
                    }

                    paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Bold(new Run("Lektioner:")));
                    paragraph.Inlines.Add(new LineBreak());

                    foreach (var lektion in valdaLektionerList)
                    {
                        paragraph.Inlines.Add(new Run(lektion.ToString()));
                        paragraph.Inlines.Add(new LineBreak());
                    }

                    document.Blocks.Add(paragraph);

                    printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Booking Confirmation");

                }
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Booking Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Rollback Transaction (pseudo-code)
                await mottagareController.RollbackTransaction();
            }
            TotalaPriset = 0;
        }, () => !string.IsNullOrEmpty(Kundnummer));



        private ICommand skrivaUtStatistik;
        public ICommand SkrivaUtStatistik => skrivaUtStatistik ??= new RelayCommand(() =>
        {
            if (SelectedMonth < 1 || SelectedMonth > 12) return; // Ensure SelectedMonth is valid

            DateTime selectedMonthDateTime = new DateTime(DateTime.Now.Year, SelectedMonth, 1);

            // Fetch all bookings for the selected month
            var activeBookings = mottagareController.HämtaBokningar()
                .Where(b => b.Aktiv && b.UtlämningsDatum.Month == selectedMonthDateTime.Month)
                .ToList();

            // Fetch all booking rows
            var allBokningsRader = mottagareController.HämtaBokningsRader();

            // Filter to get booking rows for active bookings where Logial is of type 'LagenheterI'
            var activeBookingIds = new HashSet<int>(activeBookings.Select(ab => ab.BokningID));
            var aktivaBokningsRader = allBokningsRader
                .Where(br => activeBookingIds.Contains(br.BokningID) && br.Logial?.Typ == ApartmentType.LagenheterI)
                .ToList();

            var logials = mottagareController.HämtaLogialer();
            SeriesCollection = new SeriesCollection();

            List<string> labels = new List<string>();
            ChartValues<double> values = new ChartValues<double>();

            foreach (var logial in logials)
            {
                // Calculate the number of times the current logial is booked
                int bookedCount = aktivaBokningsRader.Count(br => br.LogialID == logial.logiID);

                labels.Add(logial.logiID.ToString());
                values.Add(bookedCount);
            }

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Bookings",
                Values = values,
                DataLabels = true,
                Fill = new SolidColorBrush(Colors.Green) // Set a default color
            });

            LogialLabels = labels.ToArray();
        });

        public List<string> Månader { get; } = new List<string>
{
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
};









        private ICommand skrivaUtStatistik1;
        public ICommand SkrivaUtStatistik1 => skrivaUtStatistik1 ??= new RelayCommand(() =>
        {
            if (SelectedMonth < 1 || SelectedMonth > 12) return; // Ensure SelectedMonth is valid

            DateTime selectedMonthDateTime = new DateTime(DateTime.Now.Year, SelectedMonth, 1);

            // Fetch all bookings for the selected month
            var activeBookings = mottagareController.HämtaBokningar()
                .Where(b => b.Aktiv && b.UtlämningsDatum.Month == selectedMonthDateTime.Month)
                .ToList();

            // Fetch all booking rows
            var allBokningsRader = mottagareController.HämtaBokningsRader();

            // Filter to get booking rows for active bookings where Logial is of type 'LagenheterI'
            var activeBookingIds = new HashSet<int>(activeBookings.Select(ab => ab.BokningID));
            var aktivaBokningsRader = allBokningsRader
                .Where(br => activeBookingIds.Contains(br.BokningID) && br.Logial?.Typ == ApartmentType.LagenheterII)
                .ToList();

            var logials = mottagareController.HämtaLogialer();
            SeriesCollection = new SeriesCollection();

            List<string> labels = new List<string>();
            ChartValues<double> values = new ChartValues<double>();

            foreach (var logial in logials)
            {
                // Calculate the number of times the current logial is booked
                int bookedCount = aktivaBokningsRader.Count(br => br.LogialID == logial.logiID);

                labels.Add(logial.logiID.ToString());
                values.Add(bookedCount);
            }

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Bookings",
                Values = values,
                DataLabels = true,
                Fill = new SolidColorBrush(Colors.Green) // Set a default color
            });

            LogialLabels = labels.ToArray();
        });


        public void Generator1()
        {

            if (SelectedLektion == "Röd")
            {
                VisaTillgängligaLektionRödCommand.Execute(null!);

            }
            else if (SelectedLektion == "Grön")
            {
                VisaTillgängligaLektionGrönCommand.Execute(null!);
            }
            else if (SelectedLektion == "Blå")
            {
                VisaTillgängligaLektionBlåCommand.Execute(null!);
            }
            else if (SelectedLektion == "Svart")
            {

                VisaTillgängligaLektionSvartCommand.Execute(null!);
            }
            else if (SelectedLektion == "Privat")
            {
                VisaTillgängligaPrivatLektionCommand.Execute(null!);
            }

        }


        int senastValdLaktionRödIndex = -1;
        private RelayCommand visaTillgängligaLektionRödCommand = null!;
        public RelayCommand VisaTillgängligaLektionRödCommand => visaTillgängligaLektionRödCommand ??= new RelayCommand(() =>
        {
            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> availableLektioner = mottagareController.HämtaLektioner()
         .Where(lektion =>
             lektion.Typ == LektionTyp.Röd &&
             !mottagareController.HämtaBokningsRaderLektion()
                 .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senastValdLaktionRödIndex++;
            if (senastValdLaktionRödIndex < availableLektioner.Count)
            {
                ValdaLektioner.Add(availableLektioner[senastValdLaktionRödIndex]);
            }
            else
            {

                senastValdLaktionRödIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola selectedUtrustning = availableLektioner.FirstOrDefault();
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
        private RelayCommand visaTillgängligaLektionGrönCommand = null!;
        public RelayCommand VisaTillgängligaLektionGrönCommand => visaTillgängligaLektionGrönCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> availableLektioner = mottagareController.HämtaLektioner()
          .Where(lektion =>
              lektion.Typ == LektionTyp.Grön &&
              !mottagareController.HämtaBokningsRaderLektion()
                  .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                  .Any(bokningsRad =>
                      (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
          .ToList();

            senastValdLektionGrönIndex++;
            if (senastValdLektionGrönIndex < availableLektioner.Count)
            {
                ValdaLektioner.Add(availableLektioner[senastValdLektionGrönIndex]);
            }
            else
            {

                senastValdLektionGrönIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola selectedUtrustning = availableLektioner.FirstOrDefault();
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

        int senastValdLektionBlåIndex = -1;
        private RelayCommand visaTillgängligaLektionBlåCommand = null!;
        public RelayCommand VisaTillgängligaLektionBlåCommand => visaTillgängligaLektionBlåCommand ??= new RelayCommand(() =>
        {
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> availableLektioner = mottagareController.HämtaLektioner()
         .Where(lektion =>
             lektion.Typ == LektionTyp.Blå &&
             !mottagareController.HämtaBokningsRaderLektion()
                 .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senastValdLektionBlåIndex++;
            if (senastValdLektionBlåIndex < availableLektioner.Count)
            {
                ValdaLektioner.Add(availableLektioner[senastValdLektionBlåIndex]);
            }
            else
            {

                senastValdLektionBlåIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola selectedUtrustning = availableLektioner.FirstOrDefault();
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

        int senastValdLektionsSvartIndex = -1;
        private RelayCommand visaTillgängligaLektionSvartCommand = null!;
        public RelayCommand VisaTillgängligaLektionSvartCommand => visaTillgängligaLektionSvartCommand ??= new RelayCommand(() =>
        {
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> availableLektioner = mottagareController.HämtaLektioner()
         .Where(lektion =>
             lektion.Typ == LektionTyp.Svart &&
             !mottagareController.HämtaBokningsRaderLektion()
                 .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senastValdLektionsSvartIndex++;
            if (senastValdLektionsSvartIndex < availableLektioner.Count)
            {
                ValdaLektioner.Add(availableLektioner[senastValdLektionsSvartIndex]);
            }
            else
            {

                senastValdLektionsSvartIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola selectedUtrustning = availableLektioner.FirstOrDefault();
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

        int senastValdLektionsPrivatlektionIndex = -1;
        private RelayCommand visaTillgängligaPrivatLektionCommand = null!;
        public RelayCommand VisaTillgängligaPrivatLektionCommand => visaTillgängligaPrivatLektionCommand ??= new RelayCommand(() =>
        {
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Skidskola> availableLektioner = mottagareController.HämtaLektioner()
         .Where(lektion =>
             lektion.Typ == LektionTyp.PrivatLektion &&
             !mottagareController.HämtaBokningsRaderLektion()
                 .Where(bokningsRad => bokningsRad.SKidskolaID == lektion.skolaID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senastValdLektionsPrivatlektionIndex++;
            if (senastValdLektionsPrivatlektionIndex < availableLektioner.Count)
            {
                ValdaLektioner.Add(availableLektioner[senastValdLektionsPrivatlektionIndex]);
            }
            else
            {

                senastValdLektionsPrivatlektionIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Skidskola selectedUtrustning = availableLektioner.FirstOrDefault();
            if (!string.IsNullOrEmpty(SelectedDag))
            {


                TotalaPriset += 375;


            }
        });

        public List<string> Lektioner { get; } = new List<string>
{
    "Röd", "Blå", "Svart", "Grön", "Privat"
};
        public List<string> Dag { get; } = new List<string>
{
    "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag"
};


        public List<string> Utrustningar { get; } = new List<string>
{
    "AlpintPaket", "AlpintSkidor", "AlpintPjäxor", "AlpintStavar", "LängdSkidor", "LängdPjäxor", "LängdStavar", "LängdPaket", "PaketSnowbord", "SnowbordSkor",
    "Snowbord", "Hjälm", "Lynx 50", "Yamaha Viking", "Pulka",

};


        public void Generator()
        {

            if (SelectedUtrustning == "AlpintPaket")
            {
                VisaTillgängligaUtrustningPaketAlpintCommand.Execute(null!);

            } else if (SelectedUtrustning == "AlpintSkidor")
            {
                VisaTillgängligaUtrustningSkidorAlpintCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "AlpintPjäxor")
            {
                VisaTillgängligaUtrustningPjäxorAlpintCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "AlpintStavar")
            {

                VisaTillgängligaUtrustningStavarAlpintCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "LängdPaket")
            {
                VisaTillgängligaUtrustningPaketLängdCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "LängdSkidor")
            {
                VisaTillgängligaUtrustningLängdSkidorCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "LängdPjäxor")
            {
                VisaTillgängligaUtrustningSkidPjäxorLängdCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "LängdStavar")
            {
                VisaTillgängligaUtrustningStavarLängdCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "PaketSnowbord")
            {
                VisaTillgängligaUtrustningPaketSnowbordCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "Snowbord")
            {
                VisaTillgängligaUtrustningSnowboardCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "SnowbordSkor")
            {
                VisaTillgängligaUtrustningSnowboardSkorCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "Hjälm")
            {
                VisaTillgängligaUtrustningHjälmCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "Lynx 50")
            {
                VisaTillgängligaUtrustningLynx50Command.Execute(null!);
            }
            else if (SelectedUtrustning == "Yamaha Viking")
            {
                VisaTillgängligaUtrustningYamahaVikingCommand.Execute(null!);
            }
            else if (SelectedUtrustning == "Pulka")
            {
                VisaTillgängligaUtrustningPulkaCommand.Execute(null!);

            }
        }




        int senastValdLogialLagenhet1Index = -1;
        private RelayCommand visaTillgängligaLogialerLagenheter1Command = null!;

        public RelayCommand VisaTillgängligaLogialerLagenheter1Command => visaTillgängligaLogialerLagenheter1Command ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Logial> availableLogialer = mottagareController.HämtaLogialer()
         .Where(logial =>
             logial.Typ == ApartmentType.LagenheterI &&
             !mottagareController.HämtaBokningsRader()
                 .Where(bokningsRad => bokningsRad.LogialID == logial.logiID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();



            senastValdLogialLagenhet1Index++;
            if (senastValdLogialLagenhet1Index < availableLogialer.Count)
            {
                ValdaLogialer.Add(availableLogialer[senastValdLogialLagenhet1Index]);
            }
            else
            {

                senastValdLogialLagenhet1Index = -1;
                MessageBox.Show("Alla logialer har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            nuvarandeVecka = mottagareController.GetIso8601WeekOfYear(startTid);


            Logial selectedLogial = availableLogialer.FirstOrDefault();

            if (selectedLogial != null)
            {
                // Determine if the user has booked from Monday to Sunday


                for (DateTime currentDay = startTid; currentDay < slutTid; currentDay = currentDay.AddDays(1))
                {
                    if (currentDay.DayOfWeek == DayOfWeek.Friday || currentDay.DayOfWeek == DayOfWeek.Saturday || currentDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (nuvarandeVecka >= 14 && nuvarandeVecka <= 22)
                        {
                            TotalaPriset += 370;
                        }
                        else if (nuvarandeVecka >= 23 && nuvarandeVecka <= 50)
                        {
                            TotalaPriset += 200;
                        }
                        else if (selectedLogial.Fresön.TryGetValue(nuvarandeVecka, out int priceForDays))
                        {
                            TotalaPriset += priceForDays;
                        }
                    }
                    else
                    {
                        if (nuvarandeVecka >= 14 && nuvarandeVecka <= 22)
                        {
                            TotalaPriset += 240;
                        }
                        else if (nuvarandeVecka >= 23 && nuvarandeVecka <= 50)
                        {
                            TotalaPriset += 200;
                        }
                        else if (selectedLogial.Sönfre.TryGetValue(nuvarandeVecka, out int priceForDays))
                        {
                            TotalaPriset += priceForDays;
                        }
                    }



                }


                if (!string.IsNullOrEmpty(SelectedWeek) && BookingByWeek)
                {
                    Dictionary<int, int> priceDictionary = selectedLogial.PriserPerVecka;
                    int selectedWeekInt = int.Parse(SelectedWeek);

                    if (selectedWeekInt >= 14 && selectedWeekInt <= 22)
                    {
                        totalBokningPris1 += 1695;
                    }
                    else if (selectedWeekInt >= 23 && selectedWeekInt <= 50)
                    {
                        totalBokningPris1 += 1300;
                    }


                }
            }



            TotalaPriset += totalBokningPris + totalBokningPris1;

        });



        int senastValdLogialLagenhetIIIndex = -1;
        private RelayCommand visaTillgängligaLogialerLagenheterIICommand = null!;
        public RelayCommand VisaTillgängligaLogialerLagenheterIICommand => visaTillgängligaLogialerLagenheterIICommand ??= new RelayCommand(() =>
        {

        // Set the start and end time based on the ValtDatum and AntalDagar properties
        DateTime startTid = ValtDatum;
        int? antalDagar = AntalDagar;
        DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
        int totalBokningPris = 0;
        int totalBokningPris1 = 0;
        int nuvarandePris = 0;
        List<Logial> availableLogialer = mottagareController.HämtaLogialer()
   .Where(logial =>
       logial.Typ == ApartmentType.LagenheterII &&
       !mottagareController.HämtaBokningsRader()
           .Where(bokningsRad => bokningsRad.LogialID == logial.logiID)
           .Any(bokningsRad =>
               (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
   .ToList();


        senastValdLogialLagenhetIIIndex++;
        if (senastValdLogialLagenhetIIIndex < availableLogialer.Count)
        {
            ValdaLogialer.Add(availableLogialer[senastValdLogialLagenhetIIIndex]);
        }
        else
        {

            senastValdLogialLagenhetIIIndex = -1;
            MessageBox.Show("Alla logialer har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("Finns inga tillgängliga logialer kvar för denna datumen!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        nuvarandeVecka = mottagareController.GetIso8601WeekOfYear(startTid);


        Logial selectedLogial = availableLogialer.FirstOrDefault();

            if (selectedLogial != null)
            {
              

               

                
                for (DateTime currentDay = startTid; currentDay < slutTid; currentDay = currentDay.AddDays(1))
                {
                    


                    if (currentDay.DayOfWeek == DayOfWeek.Friday || currentDay.DayOfWeek == DayOfWeek.Saturday || currentDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (nuvarandeVecka >= 14 && nuvarandeVecka <=22)
                        {
                            TotalaPriset += 495;
                        }
                        else if (nuvarandeVecka >= 23 && nuvarandeVecka <= 50)
                        {
                            TotalaPriset += 230;
                        }
                        else if (selectedLogial.Fresön.TryGetValue(nuvarandeVecka, out int priceForDays))
                        {
                            TotalaPriset += priceForDays;
                        }
                    }
                    else
                    {
                        if (nuvarandeVecka >= 14 && nuvarandeVecka <= 22)
                        {
                            TotalaPriset += 330;
                        }
                        else if (nuvarandeVecka >= 23 && nuvarandeVecka <= 50)
                        {
                            TotalaPriset += 230;
                        }
                        else if (selectedLogial.Sönfre.TryGetValue(nuvarandeVecka, out int priceForDays))
                        {
                            TotalaPriset += priceForDays;
                        }
                    }




                }


                if (!string.IsNullOrEmpty(SelectedWeek) && BookingByWeek)
                    {
                        Dictionary<int, int> priceDictionary = selectedLogial.PriserPerVecka;
                        int selectedWeekInt = int.Parse(SelectedWeek); // Assume SelectedWeek is a valid integer

                        if (selectedWeekInt >= 14 && selectedWeekInt <= 22)
                        {
                            totalBokningPris += 1695;
                        }
                        else if (selectedWeekInt >= 23 && selectedWeekInt <= 50)
                        {
                            totalBokningPris1 += 1300;
                        }


                    }
                
                TotalaPriset += totalBokningPris + totalBokningPris1;



            }

        });
    


        int senastValdUtrusningPaketAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningPaketAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningPaketAlpintCommand => visaTillgängligaUtrustningPaketAlpintCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.PaketAlpint &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senastValdUtrusningPaketAlpintIndex++;
            if (senastValdUtrusningPaketAlpintIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningPaketAlpintIndex]);
            }
            else
            {

                senastValdUtrusningPaketAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {
                
                if (selectedUtrustning.AlpintPaket.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });


        int senastValdUtrusningSkidorAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningSkidorAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningSkidorAlpintCommand => visaTillgängligaUtrustningSkidorAlpintCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.SkidorAlpint &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senastValdUtrusningSkidorAlpintIndex++;
            if (senastValdUtrusningSkidorAlpintIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningSkidorAlpintIndex]);
            }
            else
            {

                senastValdUtrusningSkidorAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.AlpintSkidor.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningPjäxorAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningPjäxorAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningPjäxorAlpintCommand => visaTillgängligaUtrustningPjäxorAlpintCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.PjäxorAlpint &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();

            senastValdUtrusningPjäxorAlpintIndex++;
            if (senastValdUtrusningPjäxorAlpintIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningPjäxorAlpintIndex]);
            }
            else
            {

                senastValdUtrusningPjäxorAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.AlpintPjäxor.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });


        int senastValdUtrusningStavarAlpintIndex = -1;
        private RelayCommand visaTillgängligaUtrustningStavarAlpintCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningStavarAlpintCommand => visaTillgängligaUtrustningStavarAlpintCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.StavarAlpint &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senastValdUtrusningStavarAlpintIndex++;
            if (senastValdUtrusningStavarAlpintIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningStavarAlpintIndex]);
            }
            else
            {

                senastValdUtrusningStavarAlpintIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.AlpintStavar.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningPaketLängdIndex= -1;
        private RelayCommand visaTillgängligaUtrustningPaketLängdCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningPaketLängdCommand => visaTillgängligaUtrustningPaketLängdCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
                  .Where(utrustning =>
                      utrustning.Typ == UtrustningTyp.PaketLängd &&
                      !mottagareController.HämtaBokningsRaderUtrustning()
                          .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
                          .Any(bokningsRad =>
                              (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
                  .ToList();


            senastValdUtrusningPaketLängdIndex++;
            if (senastValdUtrusningPaketLängdIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningPaketLängdIndex]);
            }
            else
            {

                senastValdUtrusningPaketLängdIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.LängdPaket.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningLängdSkidorIndex = -1;
        private RelayCommand visaTillgängligaUtrustningLängdSkidorCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningLängdSkidorCommand => visaTillgängligaUtrustningLängdSkidorCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.LängdSkidor &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senastValdUtrusningLängdSkidorIndex++;
            if (senastValdUtrusningLängdSkidorIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningLängdSkidorIndex]);
            }
            else
            {

                senastValdUtrusningLängdSkidorIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.LängdSkidor.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningSkidPjäxorLängdIndex = -1;
        private RelayCommand visaTillgängligaUtrustningSkidPjäxorLängdCommand= null!;

        public RelayCommand VisaTillgängligaUtrustningSkidPjäxorLängdCommand => visaTillgängligaUtrustningSkidPjäxorLängdCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
      .Where(utrustning =>
          utrustning.Typ == UtrustningTyp.SkidPjäxorLängd &&
          !mottagareController.HämtaBokningsRaderUtrustning()
              .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
              .Any(bokningsRad =>
                  (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
      .ToList();


            senastValdUtrusningSkidPjäxorLängdIndex++;
            if (senastValdUtrusningSkidPjäxorLängdIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningSkidPjäxorLängdIndex]);
            }
            else
            {

                senastValdUtrusningSkidPjäxorLängdIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.LängdPjäxor.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningStavarLängdIndex = -1;
        private RelayCommand visaTillgängligaUtrustningStavarLängdCommand= null!;

        public RelayCommand VisaTillgängligaUtrustningStavarLängdCommand => visaTillgängligaUtrustningStavarLängdCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.StavarLängd &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senastValdUtrusningStavarLängdIndex++;
            if (senastValdUtrusningStavarLängdIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningStavarLängdIndex]);
            }
            else
            {

                senastValdUtrusningStavarLängdIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.LängdStavar.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningPaketSnowbordIndex = -1;
        private RelayCommand visaTillgängligaUtrustningPaketSnowbordCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningPaketSnowbordCommand => visaTillgängligaUtrustningPaketSnowbordCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.PaketSnowbord &&
            !mottagareController.HämtaBokningsRaderUtrustning()
                .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senastValdUtrusningPaketSnowbordIndex++;
            if (senastValdUtrusningPaketSnowbordIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningPaketSnowbordIndex]);
            }
            else
            {

                senastValdUtrusningPaketSnowbordIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.PaketSnowbord.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });
        int senastValdUtrusningSnowbordIndex = -1;
        private RelayCommand visaTillgängligaUtrustningSnowboardCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningSnowboardCommand => visaTillgängligaUtrustningSnowboardCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.Snowboard &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senastValdUtrusningSnowbordIndex++;
            if (senastValdUtrusningSnowbordIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningSnowbordIndex]);
            }
            else
            {

                senastValdUtrusningSnowbordIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.Snowbord.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningSnowboardSkorIndex = -1;
        private RelayCommand visaTillgängligaUtrustningSnowboardSkorCommand= null!;

        public RelayCommand VisaTillgängligaUtrustningSnowboardSkorCommand => visaTillgängligaUtrustningSnowboardSkorCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.SnowboardSkor &&
            !mottagareController.HämtaBokningsRaderUtrustning()
                .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();

            senastValdUtrusningSnowboardSkorIndex++;
            if (senastValdUtrusningSnowboardSkorIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningSnowboardSkorIndex]);
            }
            else
            {

                senastValdUtrusningSnowboardSkorIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.SkorSnowbord.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });


        int senastValdUtrusningHjälmIndex = -1;
        private RelayCommand visaTillgängligaUtrustningHjälmCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningHjälmCommand => visaTillgängligaUtrustningHjälmCommand ??= new RelayCommand(() =>
        {

            // vi sätter början och slut av tiden genom ValtDatum och AntalDagar egenskaper (attributer)
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
         .Where(utrustning =>
             utrustning.Typ == UtrustningTyp.Hjälm &&
             !mottagareController.HämtaBokningsRaderUtrustning()
                 .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();


            senastValdUtrusningHjälmIndex++;
            if (senastValdUtrusningHjälmIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningHjälmIndex]);
            }
            else
            {

                senastValdUtrusningHjälmIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.Hjälm.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningLynx50Index = -1;
        private RelayCommand visaTillgängligaUtrustningLynx50Command = null!;

        public RelayCommand VisaTillgängligaUtrustningLynx50Command=> visaTillgängligaUtrustningLynx50Command ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
       .Where(utrustning =>
           utrustning.Typ == UtrustningTyp.Lynx50 &&
           !mottagareController.HämtaBokningsRaderUtrustning()
               .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
               .Any(bokningsRad =>
                   (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
       .ToList();


            senastValdUtrusningLynx50Index++;
            if (senastValdUtrusningLynx50Index < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningLynx50Index]);
            }
            else
            {

                senastValdUtrusningLynx50Index = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.SkoterLynx50.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningYamahaVikingIndex= -1;
        private RelayCommand visaTillgängligaUtrustningamahaVikingCommand = null!;

        public RelayCommand VisaTillgängligaUtrustningYamahaVikingCommand => visaTillgängligaUtrustningamahaVikingCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
        .Where(utrustning =>
            utrustning.Typ == UtrustningTyp.YamahaViking &&
            !mottagareController.HämtaBokningsRaderUtrustning()
                .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
                .Any(bokningsRad =>
                    (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
        .ToList();


            senastValdUtrusningYamahaVikingIndex++;
            if (senastValdUtrusningYamahaVikingIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningYamahaVikingIndex]);
            }
            else
            {

                senastValdUtrusningYamahaVikingIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.YamahaViking.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        int senastValdUtrusningPulkaIndex= -1;
        private RelayCommand visaTillgängligaUtrustningPulkaCommand  = null!;

        public RelayCommand VisaTillgängligaUtrustningPulkaCommand => visaTillgängligaUtrustningPulkaCommand ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBokningPris = 0;
            int totalBokningPris1 = 0;
            int nuvarandePris = 0;
            List<Utrustning> availableUtrustningar = mottagareController.HämtaUtrustningar()
      .Where(utrustning =>
          utrustning.Typ == UtrustningTyp.Pulka &&
          !mottagareController.HämtaBokningsRaderUtrustning()
              .Where(bokningsRad => bokningsRad.UtrustningID == utrustning.utrustningID)
              .Any(bokningsRad =>
                  (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
      .ToList();


            senastValdUtrusningPulkaIndex++;
            if (senastValdUtrusningPulkaIndex < availableUtrustningar.Count)
            {
                ValdaUtrustningar.Add(availableUtrustningar[senastValdUtrusningPulkaIndex]);
            }
            else
            {

                senastValdUtrusningPulkaIndex = -1;
                MessageBox.Show("Alla utrustningar har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Utrustning selectedUtrustning = availableUtrustningar.FirstOrDefault();
            if (selectedUtrustning != null && antalDagar.HasValue)
            {

                if (selectedUtrustning.NilaPulka.TryGetValue(antalDagar.Value, out int priceForDays))
                {
                    TotalaPriset += priceForDays;
                }
                else
                {
                    // Handle the case when the number of days is not in the dictionary
                    MessageBox.Show($"No price found for {antalDagar.Value} days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });



      

      

        public List<string> Veckor { get; } = new List<string>
{
    "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
    "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
    "21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
    "31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
    "41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
    "51", "52"
};

       





        //Remove Commands 
        private ICommand tabortLogialCommand = null!;
        public ICommand TabortLogialCommand => tabortLogialCommand ??= new RelayCommand(() =>
        {
            if (LogialerSelectedItem != null && ValdaLogialer.Contains(LogialerSelectedItem))
            {
               
                ValdaLogialer.Remove(LogialerSelectedItem);

                TillgängligaLogialer.Add(LogialerSelectedItem);

                IsNotModified = false;
                TotalaPriset = 0;
                FöreTotalaPriset = 0;
                EfterTotalaPriset = 0;
            }
        }, () => LogialerSelectedItem != null && ValdaLogialer.Contains(LogialerSelectedItem));


        private ICommand tabortKundCommand = null!;
        public ICommand TabortKundCommand => tabortKundCommand ??= new RelayCommand(() =>
        {
            if (KunderSelectedItem != null && ValdaKunder.Contains(KunderSelectedItem))
            {
                TillgängligaKunder.Add(KunderSelectedItem);
                ValdaKunder.Remove(KunderSelectedItem);

                IsNotModified = false;
            }
        }, () => KunderSelectedItem != null && ValdaKunder.Contains(KunderSelectedItem));

        private ICommand tabortUtrustningCommand = null!;
        public ICommand TabortUtrustningCommand => tabortUtrustningCommand ??= new RelayCommand(() =>
        {
            if (UtrustningarSelectedItem != null && ValdaUtrustningar.Contains(UtrustningarSelectedItem))
            {
                TillgängligaUtrustningar.Add(UtrustningarSelectedItem);
                ValdaUtrustningar.Remove(UtrustningarSelectedItem);

                IsNotModified = false;
                TotalaPriset = 0;
            }
        }, () => UtrustningarSelectedItem != null && ValdaUtrustningar.Contains(UtrustningarSelectedItem));

        private ICommand tabortLektionerCommand = null!;
        public ICommand TabortLektionerCommand => tabortLektionerCommand ??= new RelayCommand(() =>
        {
            if (LektionerSelectedItem != null && TillgängligaLektioner.Contains(LektionerSelectedItem))
            {
                ValdaLektioner.Add(LektionerSelectedItem);
                TillgängligaLektioner.Remove(lektionerSelectedItem);

                IsNotModified = false;
            }
        }, () => LektionerSelectedItem != null && TillgängligaLektioner.Contains(LektionerSelectedItem));


        //AddCommands 

        private ICommand addLogialCommand = null!;
        public ICommand AddLogialCommand => addLogialCommand ??= new RelayCommand(() =>
        {
            if (LogialerSelectedItem != null && TillgängligaLogialer.Contains(LogialerSelectedItem))
            {
                ValdaLogialer.Add(LogialerSelectedItem);
                TillgängligaLogialer.Remove(LogialerSelectedItem);

                IsNotModified = false;

            }
        }, () => LogialerSelectedItem != null && TillgängligaLogialer.Contains(LogialerSelectedItem));


        private ICommand addKundCommand = null!;
        public ICommand AddKundCommand => addKundCommand ??= new RelayCommand(() =>
        {
            if (KunderSelectedItem != null && TillgängligaKunder.Contains(KunderSelectedItem))
            {
                ValdaKunder.Add(KunderSelectedItem);
                TillgängligaKunder.Remove(KunderSelectedItem);

                IsNotModified = false;

            }
        }, () => KunderSelectedItem != null && TillgängligaKunder.Contains(KunderSelectedItem));


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



        private ICommand readLogialerCommand = null!;
        public ICommand ReadLogialerCommand => readLogialerCommand ??= new RelayCommand(() =>
        {
            DateTime startTid = DateTime.Now; // example: current date and time
            DateTime slutTid = startTid.AddDays(7); // example: 7 days from now
            TillgängligaLogialer = new ObservableCollection<Logial>(mottagareController.HämtaLogialer());
        });




        private ICommand readKundCommand = null!;
        public ICommand ReadKundCommand => readKundCommand ??= readKundCommand = new RelayCommand(() =>
        {
            TillgängligaKunder = new ObservableCollection<Kund>(mottagareController.HämtaKunder());
        });
        private ICommand refreshCommand = null!;
        public ICommand RefreshCommand => refreshCommand ??= refreshCommand = new RelayCommand(() =>
        {
            TillgängligaKunder = new ObservableCollection<Kund>(mottagareController.HämtaKunder());
        });

        private ICommand readUtrustningCommand = null!;
        public ICommand ReadUtrustningCommand => readUtrustningCommand ??= readUtrustningCommand = new RelayCommand(() =>
        {
            TillgängligaUtrustningar = new ObservableCollection<Utrustning>(mottagareController.HämtaUtrustningar());
        });

        private ICommand uppdateraCommand = null!;
        public ICommand UppdateraCommand => uppdateraCommand ??= uppdateraCommand = new RelayCommand(() =>
        {
            TillgängligaKunder = new ObservableCollection<Kund>(mottagareController.HämtaKunder());
            Bokningar = new ObservableCollection<TellefonMottagareView>(mottagareController.HämtaBokningar());

        });
        private ICommand clearValdaCommand = null!;
        public ICommand ClearValdaCommand => clearValdaCommand ??= clearValdaCommand = new RelayCommand(() =>
        {
            ValdaLektioner.Clear();
            ValdaLogialer.Clear();  
            ValdaUtrustningar   .Clear();

        });


        private ICommand readBokningarCommand = null!;
        public ICommand ReadBokningarCommand => readBokningarCommand ??= readBokningarCommand = new RelayCommand(() =>
        {

            Bokningar = new ObservableCollection<TellefonMottagareView>(mottagareController.HämtaBokningar());
        });


        private ICommand readLektionerCommand = null!;
        public ICommand ReadLektionerCommand => readLektionerCommand ??= readLektionerCommand = new RelayCommand(() =>
        {
            TillgängligaLektioner = new ObservableCollection<Skidskola>(mottagareController.HämtaLektioner());
        });

        private ICommand sökBokningCommand = null;
        private int selectedMonth;

        public ICommand SökBokningCommand
        {
            get
            {
                if (sökBokningCommand == null)
                {
                    sökBokningCommand = new RelayCommand(() =>
                    {
                        ValdaLektioner?.Clear();
                        ValdaLogialer?.Clear();
                        ValdaUtrustningar?.Clear();

                        if (bokningsId.HasValue && int.TryParse(bokningsId.ToString(), out int parsedBookingId))
                        {
                            var hittadBokning = mottagareController?.SökBokning(parsedBookingId);
                            if (hittadBokning != null)
                            {
                                Bokningar?.Clear();
                                Bokningar?.Add(hittadBokning);

                                if (hittadBokning.BookingRaderLogialer != null)
                                {
                                    foreach (var bokningsRad in hittadBokning.BookingRaderLogialer)
                                    {
                                        var logial = mottagareController?.GetLogialById(bokningsRad.LogialID);
                                        if (logial != null)
                                        {
                                            ValdaLogialer.Add(logial);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Bokningar = new ObservableCollection<TellefonMottagareView>(mottagareController?.HämtaBokningar() ?? new List<TellefonMottagareView>());
                                // Optional error message:
                                // MessageBox.Show("Invalid booking ID", "Error");
                            }
                        }
                    });
                }
                return sökBokningCommand;
            }
        }




        private ICommand taBortBokningCommand;

        public ICommand TaBortBokningCommand
        {
            get
            {
                if (taBortBokningCommand == null)
                {
                    taBortBokningCommand = new RelayCommand(() =>
                    {
                        if (Bokningar != null && Bokningar.Count > 0)
                        {
                            var bokningAttTaBort = Bokningar[0];
                            var result = mottagareController?.TaBortBokning(bokningAttTaBort.BokningID);
                            if (result == true)
                            {
                                Bokningar.Remove(bokningAttTaBort);
                                ValdaLektioner?.Clear();
                                ValdaLogialer?.Clear();
                                ValdaUtrustningar?.Clear();
                            }
                            else
                            {
                                // Optional error message:
                                // MessageBox.Show("Could not delete booking", "Error");
                            }
                        }
                    });
                }
                return taBortBokningCommand;
            }
        }

       
        

       


      


       
        private string bokningsText;
        public string BokningsText
        {
            get { return bokningsText; }
            set
            {
                if (bokningsText != value)
                {
                    bokningsText = value;
                    OnPropertyChanged(nameof(BokningsText)); // Notify the UI about the change
                }
            }
        }

    }
}

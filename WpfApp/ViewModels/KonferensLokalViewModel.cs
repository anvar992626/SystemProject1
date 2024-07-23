using AffärsLager;
using Entiteterna;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using static Entiteterna.Konferenslokal;
using static Entiteterna.Logial;

namespace WpfApp.ViewModels
{
    public class KonferensLokalViewModel : ObservableObject
    {
        private readonly ControllerKonferensBokning konferensController;


        private ObservableCollection<Konferenslokal> tillgängligaLokaler = null!;
        public ObservableCollection<Konferenslokal> TillgängligaLokaler { get => tillgängligaLokaler; set { tillgängligaLokaler = value; OnPropertyChanged(); } }

        public ObservableCollection<Konferenslokal> valdaLokaler = null!;
        public ObservableCollection<Konferenslokal> ValdaLokaler { get => valdaLokaler; set { valdaLokaler = value; OnPropertyChanged(); } }

        public ObservableCollection<KonferensBokningView> konferensbokningar = null!;
        public ObservableCollection<KonferensBokningView> Konferensbokningar { get => konferensbokningar; set { konferensbokningar = value; OnPropertyChanged(); } }
        public ObservableCollection<FöretagKund> valdaFöretagKunder = null!;
        public ObservableCollection<FöretagKund> ValdaFöretagKunder { get => valdaFöretagKunder; set { valdaFöretagKunder = value; OnPropertyChanged(); } }

        private ObservableCollection<FöretagKund> tillgängligaFöretagKunder = new ObservableCollection<FöretagKund>();




        private Konferenslokal lokalerSelectedItem = null!;
        public Konferenslokal LokalerSelectedItem
        {
            get { return lokalerSelectedItem; }
            set { lokalerSelectedItem = value; OnPropertyChanged(); }
        }




        private Konferenslokal tillgängligaLokalerSelectedItem = null!;
        public Konferenslokal TillgängligaLokalerSelectedItem
        {
            get { return tillgängligaLokalerSelectedItem; }
            set
            {
                tillgängligaLokalerSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private FöretagKund företagkunderSelectedItem = null!;
        public FöretagKund FöretagKunderSelectedItem
        {
            get { return företagkunderSelectedItem; }
            set { företagkunderSelectedItem = value; OnPropertyChanged(); }
        }
        private FöretagKund tillgängligaFöretagKunderSelectedItem = null!;
        public FöretagKund TillgängligaFöretagKunderSelectedItem
        {
            get { return tillgängligaFöretagKunderSelectedItem; }
            set
            {
                tillgängligaFöretagKunderSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private int tillgängligaLokalerSelectedIndex;
        public int TillgängligaLokalerSelectedIndex { get { return tillgängligaLokalerSelectedIndex; } set { tillgängligaLokalerSelectedIndex = value; OnPropertyChanged(); } }


        private int lokalerSelectedIndex;
        public int LokalerSelectedIndex
        {
            get { return lokalerSelectedIndex; }
            set { lokalerSelectedIndex = value; OnPropertyChanged(); }
        }

        private int tillgängligaFöretagKunderSelectedIndex;
        public int TillgängligaFöretagKunderSelectedIndex { get { return tillgängligaFöretagKunderSelectedIndex; } set { tillgängligaFöretagKunderSelectedIndex = value; OnPropertyChanged(); } }


        private int företagkunderSelectedIndex;
        public int FöretagKunderSelectedIndex
        {
            get { return företagkunderSelectedIndex; }
            set { företagkunderSelectedIndex = value; OnPropertyChanged(); }
        }

        private bool isNotModified = true;
        public bool IsNotModified
        {
            get { return isNotModified; }
            set { isNotModified = value; OnPropertyChanged(); }
        }


        private int? totalaPriset = 0;

        public int? TotalaPriset
        {
            get { return totalaPriset; }
            set
            {
                

                totalaPriset = value;
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

        private int? konferensbokningsID;
        public int? KonferensBokningsID
        {
            get { return konferensbokningsID; }
            set { konferensbokningsID = value; OnPropertyChanged(); }
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
        public KonferensLokalViewModel()
        {
            konferensController = new ControllerKonferensBokning();
            TillgängligaLokaler = new ObservableCollection<Konferenslokal>();
            ValdaLokaler = new ObservableCollection<Konferenslokal>();
            Konferensbokningar = new ObservableCollection<KonferensBokningView>();
            ReadKonferensLokalerCommand.Execute(null!);
            ReadCommandKonferensBokningar.Execute(null!);
            ReadCommandFöretagKund.Execute(null!);
        }

        private string FöretagKundNr;
        public string Företagkundnummer
        {
            get { return FöretagKundNr; }
            set
            {
                FöretagKundNr = value;
                OnPropertyChanged();
            }
        }
        public List<string> Veckor { get; } = new List<string>
{
    "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
    "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
    "21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
    "31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
    "41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
    "51", "52"
};
        // COMMANDS 
        private int nuvarandeVecka;
        private RelayCommand skapaBokningAvLokalerCommand = null!;
        public RelayCommand SkapaBokningAvLokalerCommand => skapaBokningAvLokalerCommand ??= new RelayCommand(async () =>
        {
            try
            {
                // 1. Start Transaction (pseudo-code, adapt to your actual API)
                await konferensController.BeginTransaction();

                int företagkundNr = int.Parse(Företagkundnummer);
                DateTime startTid = ValtDatum;
                int? antalDagar = AntalDagar;
                DateTime slutTid = startTid.AddDays(antalDagar ?? 0);

                List<Konferenslokal> valdaLokalerList = ValdaLokaler.ToList();
                // 2. Perform Actions
                KonferensBokningView kb = await konferensController.CreateKonferensBookingAsync(företagkundNr, startTid, slutTid, valdaLokalerList);

                // 3. Commit Transaction (pseudo-code)
                await konferensController.CommitTransaction();

                MessageBox.Show($"Bokningen är bekräftad:\n\nFöretagskundnamn: {kb.FöretagKundNr.namn}\nStarttid: {startTid}\nAntal dagar: {antalDagar}\nSluttid: {slutTid}");

                ValdaLokaler.Clear();

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Booking Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Rollback Transaction (pseudo-code)
                await konferensController.RollbackTransaction();
            }
            //catch (Exception ex) // Generic exception to handle unexpected errors
            //{
            //    MessageBox.Show("An unexpected error occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            //    // Rollback Transaction (pseudo-code)
            //    await mottagareController.RollbackTransaction();
            //}
        }, () => !string.IsNullOrEmpty(Företagkundnummer) && AntalDagar > 0);

        int senasteValdLokal1Index = -1;
        private RelayCommand visaTillgängligaLokaler1Command = null!;

        public RelayCommand VisaTillgängligaLokaler1Command => visaTillgängligaLokaler1Command ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBookingPrice = 0;
            int totalBookingPrice1 = 0;
            int nuvarandePris = 0;
            List<Konferenslokal> availableLokaler = konferensController.HämtaKonferenslokaler()
         .Where(lokal =>
             lokal.Typ == KlokalTyp.Lokal1 &&
             !konferensController.HämtaBokningsRaderKonferens()
                 .Where(bokningsRad => bokningsRad.KonferenslokalID == lokal.konferensID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();



            senasteValdLokal1Index++;
            if (senasteValdLokal1Index < availableLokaler.Count)
            {
                ValdaLokaler.Add(availableLokaler[senasteValdLokal1Index]);
            }
            else
            {

                senasteValdLokal1Index = -1;
                MessageBox.Show("Alla konferenslokaler har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            nuvarandeVecka = konferensController.GetIso8601WeekOfYear(startTid);


            Konferenslokal selectedLokal = availableLokaler.FirstOrDefault();

            if (selectedLokal != null)
            {
                // Determine if the user has booked from Monday to Sunday


                for (DateTime currentDay = startTid; currentDay < slutTid; currentDay = currentDay.AddDays(1))
                {
                    Dictionary<int, int> priceDictionary;


                    if (currentDay.DayOfWeek == DayOfWeek.Friday || currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday
                    || currentDay.DayOfWeek == DayOfWeek.Monday || currentDay.DayOfWeek == DayOfWeek.Tuesday || currentDay.DayOfWeek == DayOfWeek.Wednesday
                    || currentDay.DayOfWeek == DayOfWeek.Thursday)
                    {
                       
                            if (nuvarandeVecka >= 14 && nuvarandeVecka <= 22)
                            {
                                TotalaPriset += 1200;
                            }
                            else if (nuvarandeVecka >= 23 && nuvarandeVecka <= 50)
                            {
                                TotalaPriset += 800;
                            }
                            else if (selectedLokal.PrisPerDygnLokal.TryGetValue(nuvarandeVecka, out int priceForDays))
                            {
                                TotalaPriset += priceForDays;
                            }
                        
                      

                    }
                    else
                    {
                        priceDictionary = selectedLokal.PrisPerTimLokal;
                    }


                   

                }


                if (!string.IsNullOrEmpty(SelectedWeek) && BookingByWeek)
                {
                    Dictionary<int, int> priceDictionary = selectedLokal.PrisPerVeckaLokal;
                    int selectedWeekInt = int.Parse(SelectedWeek); // Assume SelectedWeek is a valid integer

                    if (selectedWeekInt >= 14 && selectedWeekInt <= 22)
                    {
                        totalBookingPrice1 += 4500;
                    }
                    else if (selectedWeekInt >= 23 && selectedWeekInt <= 50)
                    {
                        totalBookingPrice1 += 3500;
                    }
                   

                }
            }

            TotalaPriset += totalBookingPrice + totalBookingPrice1;

        });

        int senastValdLokal2Index = -1;
        private RelayCommand visaTillgängligaLokalerLokal2Command = null!;

        public RelayCommand VisaTillgängligaLokalerLokal2Command => visaTillgängligaLokalerLokal2Command ??= new RelayCommand(() =>
        {

            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);
            int totalBookingPrice = 0;
            int totalBookingPrice1 = 0;
            int nuvarandePris = 0;
            List<Konferenslokal> availableLokaler = konferensController.HämtaKonferenslokaler()
         .Where(lokal =>
             lokal.Typ == KlokalTyp.Lokal2 &&
             !konferensController.HämtaBokningsRaderKonferens()
                 .Where(bokningsRad => bokningsRad.KonferenslokalID == lokal.konferensID)
                 .Any(bokningsRad =>
                     (startTid < bokningsRad.slutTid && slutTid > bokningsRad.startTid)))
         .ToList();



            senastValdLokal2Index++;
            if (senastValdLokal2Index < availableLokaler.Count)
            {
                ValdaLokaler.Add(availableLokaler[senastValdLokal2Index]);
            }
            else
            {

                senastValdLokal2Index = -1;
                MessageBox.Show("Alla konferenslokaler har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            nuvarandeVecka = konferensController.GetIso8601WeekOfYear(startTid);


            Konferenslokal selectedLokal = availableLokaler.FirstOrDefault();

            if (selectedLokal != null)
            {
                // Determine if the user has booked from Monday to Sunday


                for (DateTime currentDay = startTid; currentDay < slutTid; currentDay = currentDay.AddDays(1))
                {
                    Dictionary<int, int> priceDictionary;


                    if (currentDay.DayOfWeek == DayOfWeek.Friday || currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay.DayOfWeek == DayOfWeek.Saturday
                    || currentDay.DayOfWeek == DayOfWeek.Monday || currentDay.DayOfWeek == DayOfWeek.Tuesday || currentDay.DayOfWeek == DayOfWeek.Wednesday
                    || currentDay.DayOfWeek == DayOfWeek.Thursday)
                    {

                        if (nuvarandeVecka >= 14 && nuvarandeVecka <= 22)
                        {
                            TotalaPriset += 850;
                        }
                        else if (nuvarandeVecka >= 23 && nuvarandeVecka <= 50)
                        {
                            TotalaPriset += 650;
                        }
                        else if (selectedLokal.PrisPerDygnLokal.TryGetValue(nuvarandeVecka, out int priceForDays))
                        {
                            TotalaPriset += priceForDays;
                        }



                    }
                    else
                    {
                        priceDictionary = selectedLokal.PrisPerTimLokal;
                    }

                }


                if (!string.IsNullOrEmpty(SelectedWeek) && BookingByWeek)
                {
                    Dictionary<int, int> priceDictionary = selectedLokal.PrisPerVeckaLokal;
                    int selectedWeekInt = int.Parse(SelectedWeek); // Assume SelectedWeek is a valid integer

                    if (selectedWeekInt >= 14 && selectedWeekInt <= 22)
                    {
                        totalBookingPrice1 += 4500;
                    }
                    else if (selectedWeekInt >= 23 && selectedWeekInt <= 50)
                    {
                        totalBookingPrice1 += 3500;
                    }
                   

                }
            }

            TotalaPriset += totalBookingPrice + totalBookingPrice1;

        });

      
        //Kolla den här metod , den ser ut som en duplicering av lokal1 command? - 

        private RelayCommand visaTillgängligaLokaler2Command = null!;
        private int senasteValdLokaler12Index = -1;
        public RelayCommand VisaTillgängligaLokaler2Command => visaTillgängligaLokaler2Command ??= new RelayCommand(() =>
        {
            // Set the start and end time based on the ValtDatum and AntalDagar properties
            DateTime startTid = ValtDatum;
            int? antalDagar = AntalDagar;
            DateTime slutTid = startTid.AddDays(antalDagar ?? 0);

            // Fetch available Logialer for the date range
            // Assuming Logial object has a list of LogialBookningsDateRange, and each Booking has a StartDate and EndDate
            List<Konferenslokal> availableLokaler = konferensController.HämtaKonferenslokaler()
       .Where(availableLokaler =>
           availableLokaler.Typ == KlokalTyp.Lokal1 &&
           !konferensController.HämtaBokningsRaderKonferens()
               .Where(konferensbooking => konferensbooking.KonferenslokalID == availableLokaler.konferensID)
               .Any(konferensbooking =>
                   (startTid < konferensbooking.slutTid && slutTid > konferensbooking.startTid)))
       .ToList();


            // Increment the index and check if it's within the range of available logialer
            senasteValdLokaler12Index++;
            if (senasteValdLokaler12Index < availableLokaler.Count)
            {
                ValdaLokaler.Add(availableLokaler[senasteValdLokaler12Index]);
            }
            else
            {
                // Reset the index if we've reached the end of the availableLokaler list
                senasteValdLokaler12Index = -1;
                MessageBox.Show("Alla koferenslokaler har lagts till!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }, () => AntalDagar > 0);



        private ICommand tabortCommand = null!;
        public ICommand TabortCommand => tabortCommand ??= new RelayCommand(() =>
        {
            if (LokalerSelectedItem != null && ValdaLokaler.Contains(LokalerSelectedItem))
            {
                TillgängligaLokaler.Add(LokalerSelectedItem);
                ValdaLokaler.Remove(LokalerSelectedItem);

                IsNotModified = false;
                TotalaPriset = 0;
            }
        }, () => LokalerSelectedItem != null && ValdaLokaler.Contains(LokalerSelectedItem));

        private ICommand tabortFöretagKundCommand = null!;
        public ICommand TabortFöretagKundCommand => tabortFöretagKundCommand ??= new RelayCommand(() =>
        {
            if (FöretagKunderSelectedItem != null && ValdaFöretagKunder.Contains(FöretagKunderSelectedItem))
            {
                tillgängligaFöretagKunder.Add(FöretagKunderSelectedItem);
                ValdaFöretagKunder.Remove(FöretagKunderSelectedItem);

                IsNotModified = false;
            }
        }, () => FöretagKunderSelectedItem != null && ValdaFöretagKunder.Contains(FöretagKunderSelectedItem));


        private ICommand exitCommand = new RelayCommand(() => App.Current.Shutdown());
        public ICommand ExitCommand => exitCommand;





        private ICommand addKonferensLokalCommand = null!;
        public ICommand AddKonferensLokalCommand => addKonferensLokalCommand ??= new RelayCommand(() =>
        {
            if (LokalerSelectedItem != null && TillgängligaLokaler.Contains(LokalerSelectedItem))
            {
                ValdaLokaler.Add(LokalerSelectedItem);
                TillgängligaLokaler.Remove(LokalerSelectedItem);

                IsNotModified = false;
            }
        }, () => LokalerSelectedItem != null && TillgängligaLokaler.Contains(LokalerSelectedItem));


        private ICommand addFöretagKundCommand = null!;
        public ICommand AddFöretagKundCommand => addFöretagKundCommand ??= new RelayCommand(() =>
        {
            if (FöretagKunderSelectedItem != null && tillgängligaFöretagKunder.Contains(FöretagKunderSelectedItem))
            {
                ValdaFöretagKunder.Add(FöretagKunderSelectedItem);
                tillgängligaFöretagKunder.Remove(FöretagKunderSelectedItem);

                IsNotModified = false;
            }
        }, () => FöretagKunderSelectedItem != null && tillgängligaFöretagKunder.Contains(FöretagKunderSelectedItem));






        private ICommand readKonferensLokalerCommand = null!;
        public ICommand ReadKonferensLokalerCommand => readKonferensLokalerCommand ??= readKonferensLokalerCommand = new RelayCommand(() =>
        {
            TillgängligaLokaler = new ObservableCollection<Konferenslokal>(konferensController.HämtaKonferenslokaler());
        });


        private ICommand readCommandFöretagKund = null!;
        public ICommand ReadCommandFöretagKund => readCommandFöretagKund ??= new RelayCommand(async () =>
        {
            try
            {
                var result = await konferensController.HämtaFöretagKunderAsync();
                tillgängligaFöretagKunder = new ObservableCollection<FöretagKund>(result);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., display error message)
                Console.WriteLine(ex.Message);
            }
        });


        private ICommand readCommandKonferensBoknigar = null!;
        public ICommand ReadCommandKonferensBokningar => readCommandKonferensBoknigar ??= readCommandKonferensBoknigar = new RelayCommand(() =>
        {

            Konferensbokningar = new ObservableCollection<KonferensBokningView>(konferensController.HämtaKonfernsBokningar());
        });



        private ICommand refreshCommand = null!;
        public ICommand RefreshCommand => refreshCommand ??= new RelayCommand(async () =>
        {
            try
            {
                var result = await konferensController.HämtaFöretagKunderAsync();
                tillgängligaFöretagKunder = new ObservableCollection<FöretagKund>(result);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., display error message)
                Console.WriteLine(ex.Message);
            }
        });


        private ICommand sökKonferensBokningCommand = null;

        public ICommand SökKonferensBokningCommand
        {
            get
            {
                if (sökKonferensBokningCommand == null)
                {
                    sökKonferensBokningCommand = new RelayCommand(() =>
                    {
                        if (konferensbokningsID.HasValue && int.TryParse(konferensbokningsID.ToString(), out int parsedKonferensBookingId))
                        {
                            var hittadKonferensBokning = konferensController?.SökKonferensBokning(parsedKonferensBookingId);
                            if (hittadKonferensBokning != null)
                            {
                                Konferensbokningar?.Clear();
                                Konferensbokningar?.Add(hittadKonferensBokning);
                                ValdaLokaler?.Clear();

                                if (hittadKonferensBokning.KonferensBokingRader != null)
                                {
                                    foreach (var konferensbokningsRad in hittadKonferensBokning.KonferensBokingRader)
                                    {
                                        var konferenslokal = konferensController?.GetKonferenslokalById(konferensbokningsRad.KonferenslokalID);

                                        if (konferenslokal != null)
                                            ValdaLokaler?.Add(konferenslokal);

                                        
                                    }
                                }
                            }
                        }
                        else
                        {
                            Konferensbokningar = new ObservableCollection<KonferensBokningView>(konferensController?.HämtaKonfernsBokningar() ?? new List<KonferensBokningView>());
                            // Optional error message:
                            // MessageBox.Show("Invalid booking ID", "Error");
                        }
                    });

                }

                return sökKonferensBokningCommand;
                
            }
        }

    }
}

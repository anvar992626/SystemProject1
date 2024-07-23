using DataLager;
using Entiteterna;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace AffärsLager

{
    public class ControllerMottagare
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public Anställd AnställdLoggedIn
        {
            get; private set;
        }



        public bool LoggaIn(int AnstäldNr, string lösenord)
        {
           
            unitOfWork = new UnitOfWork(); 
            // an motsvarar AnställningsNr
            Anställd a = unitOfWork.AnställdRepos.FirstOrDefault(an => an.anställdID == AnstäldNr);
            if (a != null && a.VerifieraLösenord(lösenord))
            {
                AnställdLoggedIn = a;
                return true;
            }
            //AnställdLoggedIn = null;
            return false;
        }
        private object? currentTransaction;

        public async Task BeginTransaction()
        {
            // Pseudo-code: start a new database transaction
            // Depending on your backend, this could involve making an API call or directly starting a database transaction
            currentTransaction = new object(); // In reality, this would be your database's transaction object
        }

        public async Task CommitTransaction()
        {
            if (currentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit.");
            }

            // Pseudo-code: commit the current transaction
            // This would involve making a call to your database to commit the transaction or finalize any API calls related to the transaction
            currentTransaction = null;
        }
       
        public async Task RollbackTransaction()
        {
            if (currentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to rollback.");
            }

            // Pseudo-code: rollback the current transaction
            // This would involve making a call to your database to rollback the transaction or undo any API calls related to the transaction
            currentTransaction = null;
        }

        public async Task<DateTime?> GetLastBookingDateByKundID(int kundNr)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                //b motsvarar Bokning
                var lastBookingDate = await unitOfWork.BokningRepos
                    .Query1
                    .Where(b => b.KundNr.kundID == kundNr)
                    .OrderByDescending(b => b.UtlämningsDatum)
                    .Select(b => b.UtlämningsDatum)
                    .FirstOrDefaultAsync();

                return lastBookingDate;
            }
        }

        public IList<Kund> HämtaKunder()
        {
            return unitOfWork.KundRepos.Query().ToList();
        }
        public IList<Utrustning> HämtaUtrustningar1()
        {
            return unitOfWork.UtrustningRepos.Query().ToList();
        }

     

        public async Task<TellefonMottagareView> CreateBookingAsync(int kundNr, DateTime startTid, DateTime slutTid, List<Logial> logials, List<Utrustning> utrustningar, List<Skidskola> lektioner, int? totalpris)
        {
            var kund = unitOfWork.KundRepos.FirstOrDefault(k => k.kundID == kundNr);

            TellefonMottagareView newBooking = new TellefonMottagareView
            {
                TotalPris = (int)totalpris,
                Aktiv = true,
                KundNr = kund,
                UtlämningsDatum = startTid,
                ÅterlämningsDatum = slutTid,
                BookingRaderLogialer = logials.Select(logial => new BokningsRadLogial { Logial = logial, startTid =startTid , slutTid= slutTid, }).ToList(),
                BookingRaderUtrustningar = utrustningar.Select(utrustning => new BokningsRadUtrustning { Utrustning = utrustning, startTid = startTid, slutTid = slutTid }).ToList(),
                BookingRaderLektioner = lektioner.Select(lektion => new BokningsRadSkidskola { Skidskola = lektion, startTid = startTid, slutTid = slutTid }).ToList()
            };

           
            unitOfWork.BokningRepos.Add(newBooking);
            await unitOfWork.SaveAsync();
            return newBooking;
        }
       

      

      


        public bool TaBortBokning(int bookingId)
        {
            var booking = SökBokning(bookingId);
            if (booking != null)
            {
                unitOfWork.BokningRepos .Remove(booking);
                return true;
            }
            return false;
        }
     



        public int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public int FetchPriceForWeekRange(Dictionary<string, int> priceDictionary, int currentWeek)
        {
            foreach (var key in priceDictionary.Keys)
            {
                if (key.Contains("-"))
                {
                    var range = key.Split('-');
                    int startRange = int.Parse(range[0]);
                    int endRange = int.Parse(range[1]);

                    if (currentWeek >= startRange && currentWeek <= endRange)
                    {
                        return priceDictionary[key];
                    }
                }
            }

            // Handle the case where there's no price for the week. 
            // Return 0 or throw an exception, depending on your application's requirements.
            return 0;
        }





        public async Task<Kund> SparaKundAsync(string namn, float kreditGräns, float rabatt)
        {
            Kund k = new Kund(namn, kreditGräns, rabatt);
            unitOfWork.KundRepos.Add(k);
            await unitOfWork.SaveAsync();
            return k;
        }

       
      

        public Kund GetKundByID(int id)
        {
            // Fetch the Utrustning entity with the given ID from the database
            return unitOfWork.KundRepos.FirstOrDefault(k => k.kundID == id);
        }

        public Utrustning GetUtrustningById(int id)
        {
            // Fetch the Utrustning entity with the given ID from the database
            return unitOfWork.UtrustningRepos.FirstOrDefault(u => u.utrustningID == id);
        }
        public Logial GetLogialById(int id)
        {
            // Fetch the Logial entity with the given ID from the database
           
            return unitOfWork.LogialRepos.FirstOrDefault(l => l.logiID == id);
        }
        public Skidskola GetLektionById(int id)
        {
            // Fetch the Skidskola entity with the given ID from the database
            
            return unitOfWork.SkidskolaRepos.FirstOrDefault(s => s.skolaID == id);
        }
       

      
       
        public (DateTime, DateTime) ConvertWeekToDateTimeRange(string weekNumber)
        {
            if (int.TryParse(weekNumber, out int week))
            {
                // Assuming the year is the current year.
                // You might need to adjust this according to your requirements.
                int year = DateTime.Now.Year;

                DateTime jan1 = new DateTime(year, 1, 1);
                DateTime startOfWeek = jan1.AddDays((week - 1) * 7 - (int)jan1.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime endOfWeek = startOfWeek.AddDays(6);

                return (startOfWeek, endOfWeek);
            }
            else
            {
                throw new InvalidOperationException("Invalid week number");
            }
        }

      
        public IList<TellefonMottagareView> HämtaBokningar()
        {
            IEnumerable<TellefonMottagareView> AllaBokningar = unitOfWork.BokningRepos.Find(b => b.Aktiv);

            return AllaBokningar.ToList();
        }

      

        public IList<Logial> HämtaLogialer()
        {
         
            IEnumerable<Logial> AllaBokningar = unitOfWork.LogialRepos.GetAll();

            return AllaBokningar.ToList();
        }
        public IList<BokningsRadLogial> HämtaBokningsRader()
        {

            IEnumerable<BokningsRadLogial> AllaBokningar = unitOfWork.BokningsradRadRepos.GetAll();

            return AllaBokningar.ToList();
        }
        public IList<BokningsRadUtrustning> HämtaBokningsRaderUtrustning()
        {

            IEnumerable<BokningsRadUtrustning> AllaBokningar = unitOfWork.BokningsradRadReposUtrustning.GetAll();

            return AllaBokningar.ToList();
        }
      
        public IList<BokningsRadSkidskola> HämtaBokningsRaderLektion()
        {
            

            IEnumerable<BokningsRadSkidskola> AllaBokningar = unitOfWork.BokningsradRadReposSkidskola.GetAll();

            return AllaBokningar.ToList();
        }

       

      
        public IList<Utrustning> HämtaUtrustningar()
        {
            IEnumerable<Utrustning> AllaBokningar = unitOfWork.UtrustningRepos.GetAll();

            return AllaBokningar.ToList();
        }
        public IList<Skidskola> HämtaLektioner()
        {
            IEnumerable<Skidskola> AllaBokningar = unitOfWork.SkidskolaRepos.GetAll();

            return AllaBokningar.ToList();
        }
       
        public TellefonMottagareView SökBokning(int bokningId)
        {

            IEnumerable<TellefonMottagareView> Bokningar = unitOfWork.BokningRepos.Find(b => b.Aktiv, b => b.KundNr);
            //mbmotsvarar MottagareBokning
            TellefonMottagareView hittadBokning = Bokningar.Where(mb => mb.BokningID.Equals(bokningId)).FirstOrDefault();

            return hittadBokning;
        }


       

     

      
       
        
    }
}
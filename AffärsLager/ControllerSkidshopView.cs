using DataLager;
using Entiteterna;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffärsLager
{
    public class ControllerSkidshopView
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
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

        public async Task<SkidShopView> CreateSkidShopBokingAsync(int kundNr, DateTime startTid, DateTime slutTid, List<Utrustning> utrustningar, List<Skidskola> lektioner, int? totalpris)
        {
            var kund = unitOfWork.KundRepos.FirstOrDefault(k => k.kundID == kundNr);

            SkidShopView newSkidShopBokning = new SkidShopView
            {
                TotalPris = (int)totalpris,
                Aktiv = true,
                KundNr = kund,
                StartTid = startTid,
                Sluttid = slutTid,

                SkidshopBokningsraderUtrustningar = utrustningar.Select(utrustning => new SkidshopBokningsRadUtrustning { Utrustning = utrustning, startTid = startTid, slutTid = slutTid }).ToList(),
                SkidshopBokningsRaderSkidskola = lektioner.Select(lektion => new SkidshopBokningsRadSkidskola { Skidskola = lektion, startTid = startTid, slutTid = slutTid }).ToList()

            };


            unitOfWork.SkidshopBokningRepos.Add(newSkidShopBokning);
            await unitOfWork.SaveAsync();
            return newSkidShopBokning;
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
        public IList<SkidShopView> HämtaSkidshopBokningar()
        {
            IEnumerable<SkidShopView> AllaBokningar = unitOfWork.SkidshopBokningRepos.Find(b => b.Aktiv);



            return AllaBokningar.ToList();
        }
        public IList<SkidshopBokningsRadUtrustning> HämtaBokningsRaderUtrustningSkidshop()
        {


            IEnumerable<SkidshopBokningsRadUtrustning> AllaBokningar = unitOfWork.BokningsradRadReposUtrustningSkidshop.GetAll();

            return AllaBokningar.ToList();
        }
        public IList<SkidshopBokningsRadSkidskola> HämtaBokningsRaderLektionSkidshop()
        {


            IEnumerable<SkidshopBokningsRadSkidskola> AllaBokningar = unitOfWork.BokningsradRadReposSkidskolaSkidshop.GetAll();

            return AllaBokningar.ToList();
        }
        public SkidShopView SökSkidshopBokning(int skidhopsbokningID)
        {

            IEnumerable<SkidShopView> SkidshopBokningar = unitOfWork.SkidshopBokningRepos.Find(b => b.Aktiv, b => b.KundNr);

            SkidShopView hittadSkidshopBokning = SkidshopBokningar.Where(sb => sb.SkidshopBokningID.Equals(skidhopsbokningID)).FirstOrDefault();

            return hittadSkidshopBokning;
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
        public IList<Kund> HämtaKunder()
        {
            return unitOfWork.KundRepos.Query().ToList();
        }

    }
}

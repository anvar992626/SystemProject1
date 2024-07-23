using DataLager;
using Entiteterna;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffärsLager
{
    public class ControllerKonferensBokning
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

        public async Task<IList<FöretagKund>> HämtaFöretagKunderAsync()
        {
            return await unitOfWork.FöretagKundRepos.Query().ToListAsync();
        }
        public async Task<KonferensBokningView> CreateKonferensBookingAsync(int företagkundNr, DateTime startTid, DateTime slutTid, List<Konferenslokal> lokal)
        {
            var företagkund = unitOfWork.FöretagKundRepos.FirstOrDefault(fk => fk.företagkundID == företagkundNr);

            KonferensBokningView newKonferensBooking = new KonferensBokningView
            {
                Aktiv = true,
                FöretagKundNr = företagkund,
                UtlämningsDatum = startTid,
                ÅterlämningsDatum = slutTid,
                KonferensBokingRader = lokal.Select(lokal => new KonferensBokningsRad { Lokal = lokal, startTid = startTid, slutTid = slutTid }).ToList()
            };


            unitOfWork.KonferensBokningRepos.Add(newKonferensBooking);
            await unitOfWork.SaveAsync();
            return newKonferensBooking;
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
        public async Task<FöretagKund> SparaFöretagKund(string namn, float kreditGräns, float rabatt)
        {



            FöretagKund fb = new FöretagKund(namn, kreditGräns, rabatt);
            unitOfWork.FöretagKundRepos.Add(fb);
            await unitOfWork.SaveAsync();
            return fb;
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
        public IList<KonferensBokningsRad> HämtaBokningsRaderKonferens()
        {


            IEnumerable<KonferensBokningsRad> AllaBokningar = unitOfWork.KonferensBokningReposRad.GetAll();

            return AllaBokningar.ToList();
        }

        public KonferensBokningView SökKonferensBokning(int konferensbokningID)
        {
            IEnumerable<KonferensBokningView> KonferensBokningar = unitOfWork.KonferensBokningRepos.Find(kb => kb.Aktiv, kb => kb.FöretagKundNr);

            //hkb motsvarar "hittadKonferensBokning" 
            KonferensBokningView hittadKonferensBokning = KonferensBokningar.Where(hkb => hkb.KonferensBokningID.Equals(konferensbokningID)).FirstOrDefault();

            return hittadKonferensBokning;
        }
        public IList<KonferensBokningView> HämtaKonfernsBokningar()
        {
            IEnumerable<KonferensBokningView> AllaBokningar = unitOfWork.KonferensBokningRepos.Find(kb => kb.Aktiv, kb => kb.FöretagKundNr);
            return AllaBokningar.ToList();
        }
        public IList<Konferenslokal> HämtaKonferenslokaler()
        {
            IEnumerable<Konferenslokal> AllaBokningar = unitOfWork.KonferenslokalRepos.GetAll();

            return AllaBokningar.ToList();
        }
        public Konferenslokal GetKonferenslokalById(int id)
        {
            // Fetch the Konferens entity with the given ID from the database
            return unitOfWork.KonferenslokalRepos.FirstOrDefault(kl=> kl.konferensID == id);
        }

    }
}

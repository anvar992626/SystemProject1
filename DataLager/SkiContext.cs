using Entiteterna;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using static Entiteterna.Logial;
using static Entiteterna.Skidskola;
using static Entiteterna.Utrustning;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using static Entiteterna.Konferenslokal;
using System.Reflection.Emit;

namespace DataLager
{
    public class SkiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-VQ311F2\\SQLEXPRESS;Database=SystemProject;Trusted_Connection=True;TrustServerCertificate=True;");

            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        public SkiContext()
        {

            
        }

        public DbSet<Anställd> anställda { get; set; }
        public DbSet<TellefonMottagareView> bokningar { get; set; }
        public DbSet<SkidShopView> skidshopbokningar { get; set; }

        public DbSet<Kund> kunder { get; set; }
        public DbSet<FöretagKund> företagkunder { get; set; }
        public DbSet<Logial> logialer { get; set; }
        public DbSet<Skidskola> lektioner { get; set; }
        public DbSet<Utrustning> utrustningar { get; set; }


        public DbSet<BokningsRadLogial> bokningsRad { get; set; }
        public DbSet<KonferensBokningView> konferensBokningar { get; set; }
        
        public DbSet<Konferenslokal> konferenslokaler { get; set; }
        
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BokningsRadLogial>().Property(b => b.startTid);
            modelBuilder.Entity<BokningsRadLogial>().Property(b => b.slutTid);
            modelBuilder.Entity<BokningsRadSkidskola>().Property(b => b.startTid);
            modelBuilder.Entity<BokningsRadSkidskola>().Property(b => b.slutTid);
            modelBuilder.Entity<BokningsRadUtrustning>().Property(b => b.startTid);
            modelBuilder.Entity<BokningsRadUtrustning>().Property(b => b.slutTid);

            modelBuilder.Entity<BokningsRadLogial>().HasKey(bl => bl.BokningRadLogialID);
            modelBuilder.Entity<BokningsRadSkidskola>().HasKey(bl => bl.SkidskolaRadID);
            modelBuilder.Entity<BokningsRadUtrustning>() .HasKey(bl => bl.UtrustningsRadID);
            modelBuilder.Entity<SkidShopView>().HasKey(bl => bl.SkidshopBokningID);

            // SkidShopView
            modelBuilder.Entity<SkidshopBokningsRadSkidskola>().Property(b => b.startTid);
            modelBuilder.Entity<SkidshopBokningsRadSkidskola>().Property(b => b.slutTid);
            modelBuilder.Entity<SkidshopBokningsRadUtrustning>().Property(b => b.startTid);
            modelBuilder.Entity<SkidshopBokningsRadUtrustning>().Property(b => b.slutTid);

            modelBuilder.Entity<SkidshopBokningsRadSkidskola>().HasKey(bl => bl.RadID);
            modelBuilder.Entity<SkidshopBokningsRadUtrustning>().HasKey(bl => bl.RadID);

            //Konferensviewn
            modelBuilder.Entity<KonferensBokningsRad>().Property(b => b.startTid);
            modelBuilder.Entity<KonferensBokningsRad>().Property(b => b.slutTid);

            modelBuilder.Entity<KonferensBokningsRad>()
            .HasOne(bl => bl.KonferensBokningView)
            .WithMany(b => b.KonferensBokingRader)
            .HasForeignKey(bl => bl.KonferensBokningID);

            modelBuilder.Entity<KonferensBokningsRad>()
           .HasOne(bl => bl.Lokal)
           .WithMany(b => b.BokningsRaderKonferens)
           .HasForeignKey(bl => bl.KonferenslokalID);

            modelBuilder.Entity<DateRange>().HasNoKey();


            modelBuilder.Entity<Anställd>().HasKey(b => b.anställdID);
            modelBuilder.Entity<Anställd>().Property(b => b.namn);
            modelBuilder.Entity<Anställd>().Property(b => b.Lösenord);
            
            ////BokningMottagareView Tabell
            modelBuilder.Entity<TellefonMottagareView>().HasKey(b => b.BokningID); 
            modelBuilder.Entity<TellefonMottagareView>().Property(b => b.UtlämningsDatum);
            modelBuilder.Entity<TellefonMottagareView>().Property(b => b.ÅterlämningsDatum);
            modelBuilder.Entity<TellefonMottagareView>().Property(b => b.Aktiv);

            //KonferensBokningView
            modelBuilder.Entity<KonferensBokningView>().HasKey(b => b.KonferensBokningID); 
            modelBuilder.Entity<KonferensBokningView>().Property(b => b.UtlämningsDatum);
            modelBuilder.Entity<KonferensBokningView>().Property(b => b.ÅterlämningsDatum);

            //Skidshop 
            modelBuilder.Entity<SkidShopView>().HasKey(b => b.SkidshopBokningID); 
          
            modelBuilder.Entity<SkidShopView>().Property(b => b.StartTid);
            modelBuilder.Entity<SkidShopView>().Property(b => b.Sluttid);
            modelBuilder.Entity<SkidShopView>().Property(b => b.Aktiv);
            modelBuilder.Entity<SkidShopView>().Property(b => b.TotalPris);

            //konferenslokal
            modelBuilder.Entity<Konferenslokal>().HasKey(b => b.konferensID); 
       
            var comparer = new ValueComparer<Dictionary<int, int>>(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
            d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Konferenslokal>()
                .Property(b => b.PrisPerVeckaLokal)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(comparer);



            var comparer2 = new ValueComparer<Dictionary<int, int>>(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
            d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Konferenslokal>()
                .Property(b => b.PrisPerDygnLokal)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(comparer2);

            var comparer3 = new ValueComparer<Dictionary<int, int>>(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
            d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Konferenslokal>()
                .Property(b => b.PrisPerTimLokal)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(comparer3);



          



            //FöretagKund
            modelBuilder.Entity<FöretagKund>().HasKey(b => b.företagkundID); 
            modelBuilder.Entity<FöretagKund>().Property(b => b.namn);
            modelBuilder.Entity<FöretagKund>().Property(b => b.kreditGräns);
            modelBuilder.Entity<FöretagKund>().Property(b => b.rabatt);

            ////Kund Tabell
            modelBuilder.Entity<Kund>().HasKey(b => b.kundID); 
            
            modelBuilder.Entity<Kund>().Property(b => b.namn);
            modelBuilder.Entity<Kund>().Property(b => b.kreditGräns);
            modelBuilder.Entity<Kund>().Property(b => b.rabatt);
           
            //Logial Tabell
            // Logial Table configuration
            modelBuilder.Entity<Logial>().HasKey(b => b.logiID);
           
            modelBuilder.Entity<Logial>().Property(b => b.beskrivning);
            modelBuilder.Entity<Logial>().Property(b => b.pris);
            modelBuilder.Entity<Logial>().Property(b => b.tillgänglig);
            modelBuilder.Entity<Logial>().Property(b => b.storlek);
            modelBuilder.Entity<Logial>().Property(l => l.Typ).HasConversion<int>();
            modelBuilder.Entity<Logial>().Property(l => l.Fresön).IsRequired(false);
            modelBuilder.Entity<Logial>().Property(l => l.Sönfre).IsRequired(false);
            modelBuilder.Entity<Logial>().Property(l => l.Dag).IsRequired(false);


            var dictionaryComparer1 = new ValueComparer<Dictionary<int, int>>(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
            d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Logial>()
                .Property(b => b.PriserPerVecka)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(dictionaryComparer1);


            var dictionaryComparer2 = new ValueComparer<Dictionary<int, int>>(
          (d1, d2) => d1.SequenceEqual(d2),
          d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
          d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Logial>()
                .Property(b => b.Sönfre)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(dictionaryComparer2);



            var dictionaryComparer3 = new ValueComparer<Dictionary<int, int>>(
          (d1, d2) => d1.SequenceEqual(d2),
          d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
          d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
        );

            modelBuilder.Entity<Logial>()
                .Property(b => b.Fresön)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(dictionaryComparer3);

            var dictionaryComparer4= new ValueComparer<Dictionary<int, int>>(
         (d1, d2) => d1.SequenceEqual(d2),
         d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
         d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
        );

            modelBuilder.Entity<Logial>()
                .Property(b => b.Dag)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(dictionaryComparer4);

            modelBuilder.Entity<KonferensBokningsRad>().HasKey(b => b.KonferensBokningRadID); 

            //SkidSkola Tabell
            modelBuilder.Entity<Skidskola>().HasKey(b => b.skolaID); 
            modelBuilder.Entity<Skidskola>().Property(b => b.LärareID);   
            modelBuilder.Entity<Skidskola>().Property(b => b.tillgänglig);

            modelBuilder.Entity<Anställd>().HasKey(b => b.anställdID); 
            modelBuilder.Entity<Anställd>().Property(b => b.Lösenord);
            modelBuilder.Entity<Anställd>().Property(b => b.namn);
           

            modelBuilder.Entity<BokningsRadSkidskola>().HasKey(b => b.SkidskolaRadID);
            modelBuilder.Entity<KonferensBokningsRad>().HasKey(b => b.KonferensBokningRadID); 

            //utrusning Tabell
            modelBuilder.Entity<Utrustning>().HasKey(b => b.utrustningID); 
            modelBuilder.Entity<Utrustning>().Property(l => l.Typ).HasConversion<int>();
            modelBuilder.Entity<Utrustning>().Property(b => b.benämning).IsRequired(false);
            

           

            modelBuilder.Entity<Utrustning>()
             .Property(u => u.AlpintPaket)
             .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.AlpintPjäxor)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.AlpintSkidor)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.AlpintStavar)
            .IsRequired(false);


            modelBuilder.Entity<Utrustning>()
            .Property(u => u.LängdPaket)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.LängdSkidor)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.LängdPjäxor)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.LängdStavar)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.PaketSnowbord)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.Snowbord)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.SkorSnowbord)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.Hjälm)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.SkoterLynx50)
            .IsRequired(false);

            modelBuilder.Entity<Utrustning>()
            .Property(u => u.NilaPulka)
            .IsRequired(false);


         




            var valueComparer1 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
             );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.AlpintPaket)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer1);



            var valueComparer2 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.AlpintSkidor)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer2);



            var valueComparer3 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.AlpintPjäxor)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer3);


            var valueComparer4 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.AlpintStavar)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer4);


            var valueComparer5 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.LängdPaket)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer5);


            var valueComparer6 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.LängdSkidor)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer6);



            var valueComparer7= new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.LängdPjäxor)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer7);



            var valueComparer8 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.LängdStavar)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer8);




            var valueComparer9 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.PaketSnowbord)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer9);




            var valueComparer10 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.Snowbord)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer10);




            var valueComparer11 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.SkorSnowbord)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer11);



            var valueComparer12 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.Hjälm)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer12);




            var valueComparer13 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.SkoterLynx50)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer13);






            var valueComparer15 = new ValueComparer<Dictionary<int, int>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
            c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
            );

            modelBuilder.Entity<Utrustning>()
                .Property(u => u.NilaPulka)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, int>>(v)
                )
                .Metadata.SetValueComparer(valueComparer15);




          


   
        }

        public static void SeedData(SkiContext context)
        {
            
            // Seed Anställda if not already seeded
            if (!context.anställda.Any())
            {
                context.anställda.AddRange(
                    new Anställd
                    {
                        namn = "Skidshop Anstäld",
                       
                        Lösenord = "b"
                    },
                    new Anställd
                    {
                        namn = "Mottagare",
                       
                        Lösenord = "c"
                    },
                    new Anställd
                    {
                        namn = "Admin", Lösenord = "d"
                    },
                    new Anställd
                    {
                        namn = "Marknadschef",  Lösenord = "a"
                    }
                );

                // Save changes
                context.SaveChanges();
            }
            if (!context.utrustningar.Any())
            {
                for (int u = 6; u <= 356; u++)
                {
                    context.utrustningar.Add(
                        new Utrustning
                        {
                      
                            Typ = UtrustningTyp.SkidorAlpint,
                            benämning = "AS" + u,
                            AlpintSkidor = new Dictionary<int, int>

                            {
                {1, 180},
                {2, 305},
                {3, 405},
                {4, 495},
                {5, 560}
                            }
                        }
                    );
                }
                for (int u = 357; u <= 856; u++)
                {
                     context.utrustningar.Add(
                         new Utrustning
                        {
                            
                            Typ = UtrustningTyp.PjäxorAlpint,
                            benämning = "AP" + u,
                            AlpintPjäxor = new Dictionary<int, int>
                            {
                {1, 115},
                {2, 195},
                {3, 255},
                {4, 315},
                {5, 375}
                            }
                        }
                    );
                }
                for (int u = 857; u <= 1256; u++)
                {
                     context.utrustningar.Add(
                         new Utrustning
                        {
                            
                            Typ = UtrustningTyp.StavarAlpint,
                            benämning = "ST" + u,
                            AlpintStavar = new Dictionary<int, int>
                            {
                {1, 40},
                {2, 50},
                {3, 60},
                {4, 70},
                {5, 80}
                            }
                        }
                    );
                }
                for (int u = 1257; u <= 1341; u++)
                {
                     context.utrustningar.Add(
                        new Utrustning
                       {
                          
                           Typ = UtrustningTyp.Snowboard,
                           benämning = "SB" + u,
                           Snowbord = new Dictionary<int, int>
                           {
                {1, 190},
                {2, 335},
                {3, 455},
                {4, 555},
                {5, 625}
                           }
                       }
                   );
                }
                for (int u = 1342; u <= 1446; u++)
                {
                      context.utrustningar.Add(
                          new Utrustning
                        {
                            
                            Typ = UtrustningTyp.SnowboardSkor,
                            benämning = "SS" + u,
                            SkorSnowbord = new Dictionary<int, int>
                            {
                {1, 115},
                {2, 195},
                {3, 275},
                {4, 350},
                {5, 395}
                            }
                        }
                    );
                }
                for (int u = 1447; u <= 1581; u++)
                {
                     context.utrustningar.Add(
                        new Utrustning
                       {
                           
                           Typ = UtrustningTyp.LängdSkidor,
                           benämning = "LS" + u,
                           LängdSkidor = new Dictionary<int, int>
                           {
                {1, 100},
                {2, 170},
                {3, 220},
                {4, 270},
                {5, 320}
                           }
                       }
                   );
                }
                for (int u = 1582; u <= 1781; u++)
                {
                    context.utrustningar.Add(
                       new Utrustning
                       {
                         
                           Typ = UtrustningTyp.SkidPjäxorLängd,
                           benämning = "LP" + u,
                           LängdPjäxor = new Dictionary<int, int>
                           {
                {1, 80},
                {2, 120},
                {3, 150},
                {4, 170},
                {5, 200}
                           }
                       }
                   );
                }
                for (int u = 1782; u <= 1956; u++)
                {
                     context.utrustningar.Add(
                        new Utrustning
                       {
                         
                           Typ = UtrustningTyp.StavarLängd,
                           benämning = "SL" + u,
                           LängdStavar = new Dictionary<int, int>
                           {
                {1, 40},
                {2, 50},
                {3, 60},
                {4, 70},
                {5, 80}
                           }
                       }
                   );
                }
                for (int u = 1957; u <= 1971; u++)
                {
                    context.utrustningar.Add(
                        new Utrustning
                        {
                            
                            Typ = UtrustningTyp.SnöSkotrar,
                            benämning = "S" + u,
                            SkoterLynx50 = new Dictionary<int, int>
                            {
                        {1, 1000},
                        {3, 2750},
                        {5, 5950}
                            }
                        }
                    );
                }
              

                context.utrustningar.AddRange(
                    new Utrustning
                    {
                        
                        Typ = UtrustningTyp.Pulka,
                        NilaPulka = new Dictionary<int, int>
                        {
                {1, 240},
                {3, 640},
                {5, 1280}
                        }
                    },
                    new Utrustning
                    {
                      
                        Typ = UtrustningTyp.Hjälm,
                        Hjälm = new Dictionary<int, int>
                        {
                {1, 40},
                {2, 50},
                {3, 60},
                {4, 70},
                {5, 80}
                        }
                    },
                    new Utrustning
                    {
                      
                        Typ = UtrustningTyp.PaketSnowbord,
                        PaketSnowbord = new Dictionary<int, int>
                        {
                {1, 250},
                {2, 415},
                {3, 540},
                {4, 655},
                {5, 760}
                        }
                    },
                    new Utrustning
                    {
                        
                        Typ = UtrustningTyp.PaketLängd,
                        LängdPaket = new Dictionary<int, int>
                        {
                {1, 130},
                {2, 230},
                {3, 320},
                {4, 390},
                {5, 440}
                        }
                    },
                     new Utrustning
                     {
                         
                         Typ = UtrustningTyp.PaketAlpint,
                         AlpintPaket = new Dictionary<int, int>
                        {
                {1, 180},
                {2, 305},
                {3, 405},
                {4, 495},
                {5, 560}
                        }
                     }
                     );
                     

                // Save changes
                context.SaveChanges();
            }

            if (!context.lektioner.Any())
            {
                for (int i = 9; i <= 13; i++) // 5 groups for 5 days
                {
                    context.lektioner.Add(
                         new Skidskola
                         {
                        dagar = "privat lektion",
                        LärareID = 1,
                        tillgänglig = true,
                      
                        Typ = LektionTyp.PrivatLektion,
                        pris = 375
                    });
                }

                context.lektioner.AddRange(
                    new Skidskola
                    {
                        dagar = "mån-ons",
                        LärareID = 1,
                        tillgänglig = true,
                      
                        Typ = LektionTyp.Grön,
                        pris = 400
                    },
                    new Skidskola
                    {
                        dagar = "tor-fre",
                        LärareID = 1,
                        tillgänglig = true,
                       
                        Typ = LektionTyp.Grön,
                        pris = 500
                    },
                    new Skidskola
                    {
                        dagar = "mån-ons",
                        LärareID = 1,
                        tillgänglig = true,
                      
                        Typ = LektionTyp.Blå,
                        pris = 415
                    },
                    new Skidskola
                    {
                        dagar = "mån-ons",
                        LärareID = 1,
                        tillgänglig = true,
                      
                        Typ = LektionTyp.Röd,
                        pris = 425

                    },
                    new Skidskola
                    {
                        dagar = "tor-fre",
                        LärareID = 1,
                        tillgänglig = true,
                      
                        Typ = LektionTyp.Röd,
                        pris = 525

                    },
                    new Skidskola
                    {
                        dagar = "mån-ons",
                        LärareID = 1,
                        tillgänglig = true,
                       
                        Typ = LektionTyp.Svart,
                        pris = 455

                    },
                    new Skidskola
                    {
                        dagar = "tor-fre",
                        LärareID = 1,
                        tillgänglig = true,
                      
                        Typ = LektionTyp.Svart,
                        pris = 555

                    },
                    new Skidskola
                    {
                        dagar = "tor-fre",
                        LärareID = 1,
                        tillgänglig = true,
                        
                        Typ = LektionTyp.Blå,
                        pris = 515
                    
                     },
                    new Skidskola
                    {
                        dagar = "tor-fre",
                        LärareID = 1,
                        tillgänglig = true,
                   
                        Typ = LektionTyp.Blå,
                        pris = 515
                    }
                );

                // Save changes
                context.SaveChanges();
            }
            if (!context.logialer.Any())
            {
                var lagenheterI_fresön_prices = new Dictionary<int, int>
{
    {51, 370},
    {52, 370},
   {1, 725},
    {2, 370},
    {3, 370},
    {4, 370},
    {5, 410},
    {6, 410},
    {7, 0},
    {8, 0},
    {9, 725},
    {10, 455},
    {11, 455},
    {12, 455},
    {13, 725},
    {14-22, 370},
    {23-50, 200},

};
                var lagenheterI_sönfre_prices = new Dictionary<int, int>
{
    {51, 240},
    {52, 240},
   {1, 415},
    {2, 240},
    {3, 240},
    {4, 240},
    {5, 270},
    {6, 270},
    {7, 0},
    {8, 0},
    {9, 415},
    {10, 300},
    {11, 300},
    {12, 300},
    {13, 415},
    {14-22, 240},
    {23-50, 200},

};
                var lagenheterI_vecka_prices = new Dictionary<int, int>
{
    {51, 1695},
    {52, 1695},
   {1, 2895},
    {2, 1695},
    {3, 1695},
    {4, 1695},
    {5, 1895},
    {6, 1895},
    {7, 3895},
    {8, 3895},
    {9, 2895},
    {10, 2095},
    {11, 2095},
    {12, 2095},
    {13, 2895},
    {14-22, 1695},
    {23-50, 1300},

};
                for (int i = 1; i <= 50; i++)
                {

                    context.logialer.Add(
                           new Logial
                           {
                            
                            Typ = ApartmentType.LagenheterI,
                            storlek = 50,
                            beskrivning = "LI" + i,
                            tillgänglig = true,
                            PriserPerVecka = lagenheterI_vecka_prices,
                            Sönfre = lagenheterI_sönfre_prices,
                            Fresön = lagenheterI_fresön_prices

                        }
                    );
                }
                var lagenheter2_fresön_prices = new Dictionary<int, int>
{
    {51, 495},
    {52, 495},
   {1, 975},
    {2, 495},
    {3, 495},
    {4, 495},
    {5, 565},
    {6, 565},
    {7, 0},
    {8, 0},
    {9, 975},
    {10, 670},
    {11, 670},
    {12, 670},
    {13, 975},
    {14-22, 495},
    {23-50, 230},

};
                var lagenheter2_sönfre_prices = new Dictionary<int, int>
{
    {51, 330},
    {52, 330},
   {1, 555},
    {2, 330},
    {3, 330},
    {4, 330},
    {5, 370},
    {6, 370},
    {7, 0},
    {8, 0},
    {9, 555},
    {10, 440},
    {11, 440},
    {12, 440},
    {13, 555},
    {14-22, 330},
    {23-50, 230},

};
                var lagenheter2_vecka_prices = new Dictionary<int, int>
{
    {51, 2295},
    {52, 2295},
   {1, 3895},
    {2, 2295},
    {3, 2295},
    {4, 2295},
    {5, 2595},
    {6, 2595},
    {7, 4995},
    {8, 4995},
    {9, 3895},
    {10, 3095},
    {11, 3095},
    {12, 3095},
    {13, 3895},
    {14-22, 2295},
    {23-50, 1400},

};
                for (int i = 51; i <= 85; i++)
                {

                    context.logialer.Add(
                           new Logial
                           {
                               
                               Typ = ApartmentType.LagenheterII,
                               storlek = 70,
                               beskrivning = "LII" + i,
                               tillgänglig = true,
                               PriserPerVecka = lagenheter2_vecka_prices,
                               Sönfre = lagenheter2_sönfre_prices,
                               Fresön = lagenheter2_fresön_prices

                           }
                    );
                }

                var lagenheterI_dag_pricescamp = new Dictionary<int, int>
        {
            {51, 370},
            {52, 370},
           {1, 725},
            {2, 370},
            {3, 370},
            {4, 370},
            {5, 410},
            {6, 410},
            {7, 0},
            {8, 0},
            {9, 725},
            {10, 455},
            {11, 455},
            {12, 455},
            {13, 725},
            {14-22, 370},
            {23-50, 200},

        };
                var lagenheterI_vecka_pricescamp = new Dictionary<int, int>
        {
            {51, 815},
            {52, 815},
           {1, 1120},
            {2, 815},
            {3, 815},
            {4, 815},
            {5, 970},
            {6, 970},
            {7, 1120},
            {8, 1120},
            {9, 815},
            {10, 815},
            {11, 815},
            {12, 815},
            {13, 815},
            {14-22, 815},
            {23-50, 815},

        };
                var dag = new Dictionary<int, int>
        {
            {51, 130},
            {52, 130},
           {1, 170},
            {2, 130},
            {3, 130},
            {4, 130},
            {5, 150},
            {6, 150},
            {7, 170},
            {8, 170},
            {9, 130},
            {10, 130},
            {11, 130},
            {12, 130},
            {13, 130},
            {14-22, 130},
            {23-50, 130},

        };

                for (int i = 86; i <= 200; i++)
                {
                    context.logialer.Add(
                           new Logial
                           {
                         
                        Typ = ApartmentType.Camp,
                        beskrivning = "Camp" + i,
                        PriserPerVecka = lagenheterI_vecka_pricescamp,
                        Fresön = lagenheterI_dag_pricescamp,
                        Dag = dag

                    }
                    );
                }



                context.SaveChanges();
            }
            if (!context.konferenslokaler.Any())
            {
                var prisPerVecka50 = new Dictionary<int, int> {
               { 51, 4500 },
               { 52, 4500 },
               { 1, 7500 },
               { 2, 4500 },
               { 3, 4500 },
               { 4, 4500 },
               { 5, 4500 },
               { 6, 4500 },
               { 7, 7500 },
               { 8, 7500 },
               { 9, 4500 },
               { 10, 4500 },
               { 11, 4500 },
               { 12, 4500 },
               { 13, 7500 },
               { 14-22, 4500 },
               { 23-50, 3500 }};


                var prisPerDygn50 = new Dictionary<int, int> {
               { 51, 1200 },
               { 52, 1200 },
               { 1, 1500 },
               { 2, 1200 },
               { 3, 1200 },
               { 4, 1200 },
               { 5, 1200 },
               { 6, 1200 },
               { 7, 1500 },
               { 8, 1500 },
               { 9, 1200 },
               { 10, 1200 },
               { 11, 1200 },
               { 12, 1200 },
               { 13, 1500 },
               { 14-22, 1200 },
               { 23-50, 800 }};

                var prisPerTim50 = new Dictionary<int, int> {
               { 51, 250 },
               { 52, 250 },
               { 1, 300 },
               { 2, 250 },
               { 3, 250 },
               { 4, 250 },
               { 5, 250 },
               { 6, 250 },
               { 7, 300 },
               { 8, 300 },
               { 9, 250 },
               { 10, 250 },
               { 11, 250 },
               { 12, 250 },
               { 13, 300 },
               { 14-22, 250 },
               { 23-50, 200 }};

                var prisPerVecka20 = new Dictionary<int, int> {
               { 51, 3500 },
               { 52, 3500 },
               { 1, 6000 },
               { 2, 3500 },
               { 3, 3500 },
               { 4, 3500 },
               { 5, 3500 },
               { 6, 3500 },
               { 7, 6000 },
               { 8, 6000 },
               { 9, 3500 },
               { 10, 3500 },
               { 11, 3500 },
               { 12, 3500 },
               { 13, 6000 },
               { 14-22, 3500 },
               { 23-50, 2500 }};


                var prisPerDygn20 = new Dictionary<int, int> {
               { 51, 850 },
               { 52, 850 },
               { 1, 1150 },
               { 2, 850 },
               { 3, 850 },
               { 4, 850 },
               { 5, 850 },
               { 6, 850 },
               { 7, 1150 },
               { 8, 1150 },
               { 9, 850 },
               { 10, 850 },
               { 11, 850 },
               { 12, 850 },
               { 13, 1150 },
               { 14-22, 850 },
               { 23-50, 650 }};

                var prisPerTim20 = new Dictionary<int, int> {
               { 51, 155 },
               { 52, 155 },
               { 1, 200 },
               { 2, 155 },
               { 3, 155 },
               { 4, 155 },
               { 5, 155 },
               { 6, 155 },
               { 7, 200 },
               { 8, 200 },
               { 9, 155 },
               { 10, 155 },
               { 11, 155 },
               { 12, 155 },
               { 13, 200 },
               { 14-22, 155 },
               { 23-50, 115 }};

                // Seeding for 3 Konferenslokal of size "50 pers"
                for (int i = 0; i < 3; i++)
                {
                    context.konferenslokaler.Add(
                            new Konferenslokal
                            {
                       
                        Typ = KlokalTyp.Lokal1,
                        storlek = 50,
                        beskrivning = "KLS" + i,
                        tillgänglig = true,
                        PrisPerVeckaLokal = prisPerVecka50,
                        PrisPerDygnLokal = prisPerDygn50,
                        PrisPerTimLokal = prisPerTim50
                    });
                }

                // Seeding for 5 Konferenslokal of size "20 pers"
                for (int i = 0; i < 5; i++)
                {
                    context.konferenslokaler.Add(
                            new Konferenslokal
                            {
                       
                        Typ = KlokalTyp.Lokal2,
                        storlek = 20,
                        beskrivning = "KLL" + i,
                        tillgänglig = true,
                        PrisPerVeckaLokal = prisPerVecka20,
                        PrisPerDygnLokal = prisPerDygn20,
                        PrisPerTimLokal = prisPerTim20
                    });
                }

                // Save changes
                context.SaveChanges();
            }

        }



       
          






    }

}

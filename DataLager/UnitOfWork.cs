
using System;
using System.Collections.Generic;
using Entiteterna;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model;
namespace DataLager
{

    public class UnitOfWork : IDisposable
    {
        private SkiContext context;
        public Repository<Anställd> AnställdRepos { get; private set; }
        public Repository<TellefonMottagareView> BokningRepos { get; private set; }
       
        public Repository<Kund> KundRepos { get; private set; }
        public Repository<FöretagKund> FöretagKundRepos { get; private set; }
        public Repository<Logial> LogialRepos { get; private set; }
       
       
        public Repository<Skidskola> SkidskolaRepos { get; private set; }
        public Repository<Utrustning> UtrustningRepos { get; private set; }
        
        public Repository<Konferenslokal> KonferenslokalRepos { get; private set; }
      
       
        public Repository<KonferensBokningView> KonferensBokningRepos { get; private set; }
        public Repository<KonferensBokningsRad> KonferensBokningReposRad { get; private set; }
        public Repository<SkidShopView> SkidshopBokningRepos { get; private set; }

        public Repository<BokningsRadLogial> BokningsradRadRepos { get; private set; }
        public Repository<BokningsRadUtrustning> BokningsradRadReposUtrustning { get; private set; }
        public Repository<BokningsRadSkidskola> BokningsradRadReposSkidskola { get; private set; }
        public Repository<SkidshopBokningsRadSkidskola> BokningsradRadReposSkidskolaSkidshop { get; private set; }
        public Repository<SkidshopBokningsRadUtrustning> BokningsradRadReposUtrustningSkidshop { get; private set; }
        public UnitOfWork()
        {
            context = new SkiContext();

            BokningRepos = new Repository<TellefonMottagareView>(context);
            AnställdRepos = new Repository<Anställd>(context);
           
            KundRepos = new Repository<Kund>(context);
            FöretagKundRepos = new Repository<FöretagKund>(context);
            LogialRepos = new Repository<Logial>(context);
          
           
            SkidskolaRepos = new Repository<Skidskola>(context);
            UtrustningRepos = new Repository<Utrustning>(context);
           
            KonferenslokalRepos = new Repository<Konferenslokal>(context);
            KonferensBokningReposRad = new Repository<KonferensBokningsRad>(context);

            KonferensBokningRepos = new Repository<KonferensBokningView>(context);
            SkidshopBokningRepos = new Repository<SkidShopView>(context) ;
            BokningsradRadRepos = new Repository<BokningsRadLogial>(context);
            BokningsradRadReposUtrustning = new Repository<BokningsRadUtrustning>(context);
            BokningsradRadReposSkidskola = new Repository<BokningsRadSkidskola>(context);
            BokningsradRadReposUtrustningSkidshop = new Repository<SkidshopBokningsRadUtrustning>(context);
            BokningsradRadReposSkidskolaSkidshop = new Repository<SkidshopBokningsRadSkidskola>(context);
        }

        public async Task Save()
        {
            // ... save changes in the context
            await context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void SetEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            context.Entry(entity).State = state;
        }
        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Attach(entity);
        }
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return context.Entry(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }


    }
}
using Entiteterna;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DataLager
{
    public class Repository<T>
        where T : class
    {
        internal SkiContext context;
        internal DbSet<T> dbSet;
        
        public Repository(SkiContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
       

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.Where(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefault(predicate);
        }

        public IQueryable<T> Query(params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public bool Remove(T entity)
        {
            dbSet.Remove(entity);
            int rowsAffected = context.SaveChanges();
            return rowsAffected > 0;
        }

        public int Count()
        {
            return dbSet.Count();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }
        public IQueryable<T> Query1 => dbSet.AsQueryable();


    }
}
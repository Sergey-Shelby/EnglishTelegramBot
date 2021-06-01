using EnglishTelegramBot.Common.Abstractions.Repositories;
using EnglishTelegramBot.Database.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Repositories
{
	public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private EnglishContext db;
        private DbSet<T> dbSet;

        public BaseRepository(EnglishContext dbContext)
        {
            this.db = dbContext;
            this.dbSet = db.Set<T>();
        }

        public IEnumerable<T> FetchAll()
        {
            return dbSet.AsNoTracking();
        }

        public T FetchById(int id)
        {
            return dbSet.Find(id);
        }

        public void Create(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            db.Attach(t);
            db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T t = dbSet.Find(id);
            if (t != null)
                dbSet.Remove(t);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

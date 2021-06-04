using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<T>> FetchAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> FetchByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task CreateAsync(T t)
        {
            await dbSet.AddAsync(t);
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

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
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

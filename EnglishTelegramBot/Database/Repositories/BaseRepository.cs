using EnglishTelegramBot.Common.Abstractions.Repositories;
using EnglishTelegramBot.Database.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Repositories
{
	abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private EnglishContext db;

        public BaseRepository()
        {
            this.db = new EnglishContext();
        }

        public IEnumerable<T> FetchAll()
        {
            return db.Set<T>();
        }

        public T FetchById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Create(T t)
        {
            db.Set<T>().Add(t);
        }

        public void Update(T t)
        {
            db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T t = db.Set<T>().Find(id);
            if (t != null)
                db.Set<T>().Remove(t);
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

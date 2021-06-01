using EnglishTelegramBot.Common.Abstractions.Repositories;
using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	public class ThemeRepository : IThemeRepository<Theme> 
    {
        private EnglishContext db;

        public ThemeRepository()
        {
            this.db = new EnglishContext();
        }

        public IEnumerable<Theme> FetchAll()
        {
            return db.Themes;
        }

        public Theme FetchById(int id)
        {
            return db.Themes.Find(id);
        }

        public void Create(Theme theme)
        {
            db.Themes.Add(theme);
        }

        public void Update(Theme theme)
        {
            db.Entry(theme).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Theme theme = db.Themes.Find(id);
            if (theme != null)
                db.Themes.Remove(theme);
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

using EnglishTelegramBot.Common.Abstractions.Repositories;
using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	public class PartOfSpeechRepository : IPartOfSpeechRepository<PartOfSpeech>
    {
        private EnglishContext db;

        public PartOfSpeechRepository()
        {
            this.db = new EnglishContext();
        }

        public IEnumerable<PartOfSpeech> GetPartOfSpeechList()
        {
            return db.PartsOfSpeech;
        }

        public PartOfSpeech GetPartOfSpeech(int id)
        {
            return db.PartsOfSpeech.Find(id);
        }

        public void Create(PartOfSpeech partOfSpeech)
        {
            db.PartsOfSpeech.Add(partOfSpeech);
        }

        public void Update(PartOfSpeech partOfSpeech)
        {
            db.Entry(partOfSpeech).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PartOfSpeech partOfSpeech = db.PartsOfSpeech.Find(id);
            if (partOfSpeech != null)
                db.PartsOfSpeech.Remove(partOfSpeech);
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

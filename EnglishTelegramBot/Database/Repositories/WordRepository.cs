using EnglishTelegramBot.Common.Abstractions.Repositories;
using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordRepository : IWordRepository<Word>
	{
        private EnglishContext db;

        public WordRepository()
        {
            this.db = new EnglishContext();
        }

        public IEnumerable<Word> FetchAll()
        {
            return db.Words;
        }

        public Word FetchById(int id) 
        {
            return db.Words.Find(id);
        }

        public void Create(Word word)
        {
            db.Words.Add(word);
        }

        public void Update(Word word)
        {
            db.Entry(word).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Word word = db.Words.Find(id);
            if (word != null)
                db.Words.Remove(word);
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

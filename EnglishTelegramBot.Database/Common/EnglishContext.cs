﻿using EnglishTelegramBot.DomainCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishTelegramBot.Database.Common
{
    public class EnglishContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<PartOfSpeech> PartsOfSpeech { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WordTraining> WordTraining { get; set; }
        public DbSet<WordPartOfSpeech> WordPartOfSpeech { get; set; }
        public DbSet<WordPartOfSpeechData> WordPartOfSpeechData { get; set; }
        public DbSet<ThemeWords> ThemeWords { get; set; }
        public DbSet<WordTrainingSet> WordTrainingSet { get; set; }

        public EnglishContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("workstation id=EnglishDB.mssql.somee.com;packet size=4096;user id=SergeyShelby_SQLLogin_2;pwd=f8f29t4bkb;data source=EnglishDB.mssql.somee.com;persist security info=False;initial catalog=EnglishDB");
        }
    }
}

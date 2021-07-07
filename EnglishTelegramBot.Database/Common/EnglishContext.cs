using EnglishTelegramBot.DomainCore.Entities;
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
        public DbSet<LearnWord> LearnWord { get; set; }

        public EnglishContext(DbContextOptions<EnglishContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}

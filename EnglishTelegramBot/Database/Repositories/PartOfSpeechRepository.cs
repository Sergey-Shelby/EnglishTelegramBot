using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	public class PartOfSpeechRepository : BaseRepository<PartOfSpeech>, IPartOfSpeechRepository
    {
		public PartOfSpeechRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}

using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	public class ThemeRepository : BaseRepository<Theme>, IThemeRepository
    {
		public ThemeRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}

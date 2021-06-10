using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Framework
{
    public interface IQuery
    {
    }

    public interface IQueryHandler<in TQuery, TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}

using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Framework
{
    public interface ICommand
    {
    }

    public interface ICommandResultHandler<in TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}

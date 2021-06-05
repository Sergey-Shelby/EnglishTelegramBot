namespace Telegraf.Net.Abstractions
{
    public interface IBotBuilder
    {
        IBotBuilder Use<TCommand>() where TCommand : IAnswerCommand;
        IBotBuilder Use<TCommand>(TCommand handler) where TCommand : IAnswerCommand;
        UpdateDelegate Build(UpdateDelegate handle = null);
    }
}

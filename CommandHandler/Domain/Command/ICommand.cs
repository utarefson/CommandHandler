using CommandHandler.Domain.Common;

namespace CommandHandler.Domain.Command
{
    public interface ICommand
    {
        CommandExecutionResult Execute();
    }
}

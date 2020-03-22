using CommandHandler.Domain.Common;

namespace CommandHandler.Domain.Command
{
    public static class CommandExecutor
    {
        public static CommandExecutionResult Execute(ICommand command)
        {
            return command.Execute();
        }
    }
}

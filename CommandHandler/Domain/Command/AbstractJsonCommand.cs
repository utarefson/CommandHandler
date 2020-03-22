using CommandHandler.Domain.Common;

namespace CommandHandler.Domain.Command
{
    public abstract class AbstractJsonCommand : ICommand
    {
        protected string jsonData;

        public AbstractJsonCommand(string jsonData)
        {
            this.jsonData = jsonData;
        }

        public CommandExecutionResult Execute()
        {
            return this.PerformExecution();
        }

        protected abstract CommandExecutionResult PerformExecution();
    }
}

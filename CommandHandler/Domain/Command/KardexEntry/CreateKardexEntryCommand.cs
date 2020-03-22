using CommandHandler.Domain.Common;
using CommandHandler.Domain.Entity;
using Newtonsoft.Json;

namespace CommandHandler.Domain.Command.KardexEntry
{
    public class CreateKardexEntryCommand : AbstractJsonCommand
    {
        public CreateKardexEntryCommand(string jsonData) : base(jsonData)
        {
        }

        protected override CommandExecutionResult PerformExecution()
        {
            CreateKardexEntryCommandJson jsonObject = JsonConvert.DeserializeObject<CreateKardexEntryCommandJson>(this.jsonData);
            IEntity entity = new KardexEntryEntity(jsonObject);
            return (entity.Create()) ? CommandExecutionResult.Succeed : CommandExecutionResult.Fail;
        }
    }
}

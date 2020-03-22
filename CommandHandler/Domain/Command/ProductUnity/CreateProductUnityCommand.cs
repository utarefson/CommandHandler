using CommandHandler.Domain.Common;
using CommandHandler.Domain.Entity;
using Newtonsoft.Json;

namespace CommandHandler.Domain.Command.ProductUnity
{
    public class CreateProductUnityCommand : AbstractJsonCommand
    {
        public CreateProductUnityCommand(string jsonData) : base(jsonData)
        {
        }

        protected override CommandExecutionResult PerformExecution()
        {
            CreateProductUnityCommandJson jsonObject = JsonConvert.DeserializeObject<CreateProductUnityCommandJson>(this.jsonData);
            IEntity entity = new ProductUnityEntity(jsonObject);
            return (entity.Create()) ? CommandExecutionResult.Succeed : CommandExecutionResult.Fail;
        }
    }
}

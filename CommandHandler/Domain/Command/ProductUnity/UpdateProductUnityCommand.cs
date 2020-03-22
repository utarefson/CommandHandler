using CommandHandler.Domain.Common;
using CommandHandler.Domain.Entity;
using Newtonsoft.Json;

namespace CommandHandler.Domain.Command.ProductUnity
{
    public class UpdateProductUnityCommand : AbstractJsonCommand
    {
        public UpdateProductUnityCommand(string jsonData) : base(jsonData)
        {
        }

        protected override CommandExecutionResult PerformExecution()
        {
            CreateProductUnityCommandJson jsonObject = JsonConvert.DeserializeObject<CreateProductUnityCommandJson>(this.jsonData);
            IEntity entity = new ProductUnityEntity(jsonObject);
            return (entity.Update()) ? CommandExecutionResult.Succeed : CommandExecutionResult.Fail;
        }
    }
}

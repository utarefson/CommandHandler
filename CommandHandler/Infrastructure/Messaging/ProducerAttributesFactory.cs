using Newtonsoft.Json;

namespace CommandHandler.Infrastructure.Messaging
{
    public static class ProducerAttributesFactory<T>
    {
        public static IProducerAttributes Create(string routingKey, T jsonObject)
        {
            string jsonData = JsonConvert.SerializeObject(
                jsonObject,
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );

            return new EventBusQueueAttributes(routingKey, jsonData);
        }
    }
}

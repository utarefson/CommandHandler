
using System.Text;

namespace CommandHandler.Infrastructure.Messaging
{
    public class EventBusQueueAttributes : IProducerAttributes
    {
        private const string QUEUE_NAME = "EventBusQueue";
        private string routingKey;
        private string jsonData;

        public EventBusQueueAttributes(string routingKey, string jsonData)
        {
            this.routingKey = routingKey;
            this.jsonData = jsonData;
        }

        public string GetExchangeName()
        {
            return string.Format("{0}Exchange", this.routingKey);
        }

        public string GetQueueName()
        {
            return QUEUE_NAME;
        }

        public string GetRoutingKey()
        {
            return this.routingKey;
        }

        public byte[] GetMessageBody()
        {
            return Encoding.UTF8.GetBytes(this.jsonData);
        }
    }
}

using RabbitMQ.Client;

namespace CommandHandler.Infrastructure.Messaging
{
    public class EventBusQueueProducer
    {
        private IModel channelForEventing;

        public EventBusQueueProducer()
        {
            this.SetupChannel();
        }

        private void SetupChannel()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            IConnection connection = connectionFactory.CreateConnection();
            this.channelForEventing = connection.CreateModel();
            this.channelForEventing.BasicQos(0, 1, false);
        }
        
        public void Publish(IProducerAttributes attributes)
        {
            this.channelForEventing.BasicPublish(
                attributes.GetExchangeName(),
                attributes.GetRoutingKey(),
                this.channelForEventing.CreateBasicProperties(),
                attributes.GetMessageBody()
            );
        }
    }
}

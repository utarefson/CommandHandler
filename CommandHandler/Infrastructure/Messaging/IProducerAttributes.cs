namespace CommandHandler.Infrastructure.Messaging
{
    public interface IProducerAttributes
    {
        string GetQueueName();
        
        string GetExchangeName();

        string GetRoutingKey();

        byte[] GetMessageBody();
    }
}

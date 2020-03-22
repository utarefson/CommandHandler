using System;
using CommandHandler.Domain.Event;
using CommandHandler.Infrastructure.Messaging;
using CommandHandler.Infrastructure.Repository;
using CommandHandler.Domain.Command.ProductUnity;

namespace CommandHandler.Domain.Entity
{
    public class ProductUnityEntity : IEntity
    {
        public static int Seed = 0;

        private int id;
        private string name;
        private string description;

        public ProductUnityEntity(CreateProductUnityCommandJson data)
        {
            this.id = ++Seed;
            this.name = data.Name;
            this.description = data.Description;
        }

        public bool Create()
        {
            string routingKey = "CreateProductUnityEvent";
            CreateProductUnityEventJson jsonObject = this.CreateJsonObject(routingKey);

            // Add to the EventStore
            bool stored = RepositoryFactory<CreateProductUnityEventJson>.Create().Append(jsonObject);

            // Publish to RabbitMQ
            if (stored)
            {
                jsonObject.Event = null;

                IProducerAttributes attributes = ProducerAttributesFactory<CreateProductUnityEventJson>.Create(routingKey, jsonObject);

                EventBusQueueProducer producer = new EventBusQueueProducer();
                producer.Publish(attributes);
            }

            return stored;
        }

        private CreateProductUnityEventJson CreateJsonObject(string routingKey)
        {
            CreateProductUnityEventJson jsonObject = new CreateProductUnityEventJson();
            jsonObject.Event = routingKey;
            jsonObject.Id = this.id;
            jsonObject.Name = this.name;
            jsonObject.Description = this.description;

            return jsonObject;
        }

        public bool Update()
        {
            string routingKey = "UpdateProductUnityEvent";
            CreateProductUnityEventJson jsonObject = this.CreateJsonObject(routingKey);

            // Add to the EventStore
            return RepositoryFactory<CreateProductUnityEventJson>.Create().Append(jsonObject);
        }

        public bool Delete()
        {
            string routingKey = "DeleteProductUnityEvent";
            CreateProductUnityEventJson jsonObject = this.CreateJsonObject(routingKey);

            // Add to the EventStore
            return RepositoryFactory<CreateProductUnityEventJson>.Create().Append(jsonObject);
        }
    }
}

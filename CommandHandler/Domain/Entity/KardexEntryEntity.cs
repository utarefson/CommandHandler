using CommandHandler.Domain.Command.KardexEntry;
using CommandHandler.Domain.Event;
using CommandHandler.Infrastructure.Messaging;
using CommandHandler.Infrastructure.Repository;
using System;

namespace CommandHandler.Domain.Entity
{
    public class KardexEntryEntity : IEntity
    {
        public static int Seed = 0;

        private int id;
        private long kardexEntryDate;
        private int productId;
        private string description;
        private int quantity;
        private int price;
        private int isASale;

        public KardexEntryEntity(CreateKardexEntryCommandJson data)
        {
            this.id = ++Seed;
            this.kardexEntryDate = data.KardexEntryDate;
            this.productId = data.ProductId;
            this.description = data.Description;
            this.quantity = data.Quantity;
            this.price = data.Price;
            this.isASale = data.IsASale;
        }

        public bool Create()
        {
            string routingKey = "CreateKardexEntryEvent";
            CreateKardexEntryEventJson jsonObject = this.CreateJsonObject(routingKey);

            // Add to the EventStore
            bool stored = RepositoryFactory<CreateKardexEntryEventJson>.Create().Append(jsonObject);

            // Publish to RabbitMQ
            if (stored)
            {
                jsonObject.Event = null;

                IProducerAttributes attributes = ProducerAttributesFactory<CreateKardexEntryEventJson>.Create(routingKey, jsonObject);

                EventBusQueueProducer producer = new EventBusQueueProducer();
                producer.Publish(attributes);
            }

            return stored;
        }

        private CreateKardexEntryEventJson CreateJsonObject(string routingKey)
        {
            CreateKardexEntryEventJson jsonObject = new CreateKardexEntryEventJson();
            jsonObject.Event = routingKey;
            jsonObject.Id = this.id;
            jsonObject.KardexEntryDate = this.kardexEntryDate;
            jsonObject.ProductId = this.productId;
            jsonObject.Description = this.description;
            jsonObject.Quantity = this.quantity;
            jsonObject.Price = this.price;
            jsonObject.IsASale = this.isASale;

            return jsonObject;
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}

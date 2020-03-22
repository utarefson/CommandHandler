using CommandHandler.Domain.Command;
using CommandHandler.Domain.Command.KardexEntry;
using CommandHandler.Domain.Command.ProductUnity;
using CommandHandler.Domain.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommandHandler.Infrastructure.Messaging
{
    public class CommandBusQueueConsumer
    {
        private const string QUEUE_NAME = "CommandBusQueue";
        private IModel channelForEventing;

        private Dictionary<string, RabbitMQCommand> commandMap;
        private Dictionary<RabbitMQCommand, Func<string, CommandExecutionResult>> commandFunctionMap;

        public CommandBusQueueConsumer()
        {
            this.PopulateCommandMap();
            this.PopulateCommandFucntionMap();
            this.SetupChannel();
        }

        private void PopulateCommandMap()
        {
            this.commandMap = new Dictionary<string, RabbitMQCommand>();

            var values = Enum.GetValues(typeof(RabbitMQCommand)).Cast<RabbitMQCommand>();
            foreach (RabbitMQCommand command in values)
            {
                this.commandMap.Add(command.ToString(), command);
            }
        }

        private void PopulateCommandFucntionMap()
        {
            this.commandFunctionMap = new Dictionary<RabbitMQCommand, Func<string, CommandExecutionResult>>()
            {
                // ProductUnity Commands
                {
                    RabbitMQCommand.CreateProductUnityCommand,
                    (jsonData) => { return CommandExecutor.Execute(new CreateProductUnityCommand(jsonData)); }
                },
                {
                    RabbitMQCommand.UpdateProductUnityCommand,
                    (jsonData) => { return CommandExecutor.Execute(new UpdateProductUnityCommand(jsonData)); }
                },
                {
                    RabbitMQCommand.DeleteProductUnityCommand,
                    (jsonData) => { return CommandExecutor.Execute(new DeleteProductUnityCommand(jsonData)); }
                },

                // KardexEntry Commands
                {
                    RabbitMQCommand.CreateKardexEntryCommand,
                    (jsonData) => { return CommandExecutor.Execute(new CreateKardexEntryCommand(jsonData)); }
                }

            };
        }
        
        private void SetupChannel()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            /*connectionFactory.Port = 15672;
            connectionFactory.HostName = "localhost";
            connectionFactory.UserName = "guest";
            connectionFactory.Password = "guest";
            connectionFactory.VirtualHost = "accounting";*/

            IConnection connection = connectionFactory.CreateConnection();
            this.channelForEventing = connection.CreateModel();
            this.channelForEventing.BasicQos(0, 1, false);
        }

        public void Start()
        {
            EventingBasicConsumer eventingBasicConsumer = new EventingBasicConsumer(this.channelForEventing);
            eventingBasicConsumer.Received += this.Received;

            this.channelForEventing.BasicConsume(QUEUE_NAME, false, eventingBasicConsumer);
        }

        private void Received(object sender, BasicDeliverEventArgs e)
        {
            string commandString = e.RoutingKey;
            string jsonData = Encoding.UTF8.GetString(e.Body);
            if (this.commandMap.ContainsKey(commandString))
            {
                RabbitMQCommand command = this.commandMap[commandString];
                CommandExecutionResult result = this.commandFunctionMap[command](jsonData);
                
                string status = (result == CommandExecutionResult.Fail) ? "Successfully :)" : "With Errors :(";
                string[] lines = new string[] { string.Format("RabbitMQ -> Command handled {0}", status) };

                File.AppendAllLines(@"C:\Temp\CommandHandlerLog.txt", lines);
            }

            // We are going to consume all the messages
            this.channelForEventing.BasicAck(e.DeliveryTag, false);
        }
    }
}

using CommandHandler.Infrastructure.Messaging;
using System;
using System.IO;
using System.Timers;

namespace CommandHandler
{
    public class CommandHandlerCore
    {
        private readonly Timer timer;
        private CommandBusQueueConsumer consumer;

        public CommandHandlerCore()
        {
            this.timer = new Timer(5000) { AutoReset = true };
            this.timer.Elapsed += TimerElapsed;

            this.consumer = new CommandBusQueueConsumer();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs eventArgs)
        {
            string[] lines = new string[] { DateTime.UtcNow.ToString() };

            File.AppendAllLines(@"C:\Temp\CommandHandlerLog.txt", lines);
        }
        
        public void Start()
        {
            this.timer.Start();
            this.consumer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }
    }
}

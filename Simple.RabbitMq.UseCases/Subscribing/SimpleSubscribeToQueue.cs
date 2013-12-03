using System;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using Simple.RabbitMq.UseCases.Messages;

namespace Simple.RabbitMq.UseCases.Subscribing
{
    public class SimpleSubscribeToQueue
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using (var connectionFactory = new DisposableConnectionFactory())
            using (var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            using (var subscriptionFactory = new SubscriptionFactory())
            using (var subscription = subscriptionFactory.Create(channel:channel,queueName:"TheUsableMessageQueue"))
            {
                
                foreach (var message in subscription.Messages)
                {
                    var messageBody = message.BodyAsJson<UsableMessage>();
                    ProcessMessage(messageBody);
                    
                    subscription.Ack(message);
                }
            }
        }

        private static void ProcessMessage(UsableMessage message)
        {
            Console.WriteLine(message.Description);
        }
    }
}
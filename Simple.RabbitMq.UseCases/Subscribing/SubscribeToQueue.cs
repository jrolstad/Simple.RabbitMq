using RabbitMQ.Client.MessagePatterns;
using Simple.RabbitMq.UseCases.Messages;

namespace Simple.RabbitMq.UseCases.Subscribing
{
    public class SubscribeToQueue
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
                foreach (var message in subscription)
                {
                    subscription.LatestEvent.
                }

               
            }
        }
    }
}
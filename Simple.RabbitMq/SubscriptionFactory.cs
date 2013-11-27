using System;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace Simple.RabbitMq
{
    public class SubscriptionFactory:IDisposable
    {
        public virtual Subscription Create(IModel channel, string queueName)
        {
            return new Subscription(channel,queueName);
        }

        public void Dispose()
        {
            
        }
    }
}
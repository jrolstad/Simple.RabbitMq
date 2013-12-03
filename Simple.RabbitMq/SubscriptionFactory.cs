using System;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace Simple.RabbitMq
{
    public class SubscriptionFactory:IDisposable
    {
        public virtual MockableSubscription Create(IModel channel, string queueName)
        {
            return new MockableSubscription(channel, queueName);
        }

        public void Dispose()
        {
            
        }
    }
}
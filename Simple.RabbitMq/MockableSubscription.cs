using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace Simple.RabbitMq
{
    public class MockableSubscription:Subscription
    {
        public MockableSubscription(IModel model, string queueName) : base(model, queueName)
        {
        }

        public MockableSubscription(IModel model, string queueName, bool noAck) : base(model, queueName, noAck)
        {
        }

       
    }
}
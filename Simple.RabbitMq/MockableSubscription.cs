using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

        public virtual IEnumerable<BasicDeliverEventArgs> Messages
        {
            get
            {
                return this.Cast<BasicDeliverEventArgs>();
            }
        }
    }
}
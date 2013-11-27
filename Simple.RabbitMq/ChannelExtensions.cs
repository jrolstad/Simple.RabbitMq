using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Simple.RabbitMq
{
    public static class ChannelExtensions
    {
        public static IModel PublishAsJson<T>(this IModel channel, T message, string exchange, string routingKey = null)
        {
            var binaryMessage = ConvertTypeToJsonBytes(message);

            Publish(channel, binaryMessage, exchange, routingKey);

            return channel;
        }

        public static IModel PublishCollectionAsJson<T>(this IModel channel, IEnumerable<T> messages, string exchange, string routingKey = null)
        {
            foreach (var message in messages)
            {
                PublishAsJson(channel, message, exchange, routingKey);
            }

            return channel;
        }

        public static IModel PublishCollectionAsJsonAsync<T>(this IModel channel, IEnumerable<T> messages, string exchange, string routingKey = null)
        {
            Parallel.ForEach(messages, m => PublishAsJson(channel, m, exchange, routingKey));

            return channel;
        }

        public static IModel QueueDeclare<T>(this IModel channel, bool isDurable = false, bool isExclusive = false, bool isAutoDelete = false)
        {
            var queueName = typeof (T).FullName;
            channel.QueueDeclare(queue: queueName, durable: isDurable, exclusive: isExclusive, autoDelete: isAutoDelete,arguments:null);

            return channel;
        }

        public static IModel ExchangeDeclare<T>(this IModel channel, string exchangeType)
        {
            var exchangeName = typeof(T).FullName;
            channel.ExchangeDeclare(exchange:exchangeName,type:exchangeType);

            return channel;
        }

        public static IModel QueueBind<T>(this IModel channel, string routingKey=null)
        {
            var name = typeof(T).FullName;
            var queueName = name;
            if (!string.IsNullOrWhiteSpace(routingKey))
            {
                queueName = string.Format("{0}.{1}", name, routingKey);
            }
            channel.QueueBind(queue: queueName, exchange: name, routingKey: routingKey ?? string.Empty);

            return channel;
        }

        public static IModel SetMessageFetchSize(this IModel channel, int size)
        {
            var preFetchCount = (ushort) size;
            channel.BasicQos(prefetchSize: 0, prefetchCount: preFetchCount, global: false);

            return channel;
        }

        private static byte[] ConvertTypeToJsonBytes<T>(T message)
        {
            var messageAsString = JsonConvert.SerializeObject(message);
            var binaryMessage = Encoding.UTF8.GetBytes(messageAsString);
            return binaryMessage;
        }

        private static void Publish(this IModel channel, byte[] message, string exchange, string routingKey)
        {
            var routingKeyValue = routingKey ?? string.Empty;

            channel.BasicPublish(exchange: exchange, routingKey: routingKeyValue, basicProperties: null, body: message);

        }
    }
}
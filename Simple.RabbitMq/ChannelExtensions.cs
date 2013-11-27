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
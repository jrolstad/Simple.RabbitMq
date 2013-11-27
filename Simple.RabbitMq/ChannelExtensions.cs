using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Simple.RabbitMq
{
    public static class ChannelExtensions
    {
        public static IModel PublishAsJson<T>(this IModel channel, T message, string exchange, string routingKey = null)
        {
            var messageAsString = JsonConvert.SerializeObject(message);
            var binaryMessage = Encoding.UTF8.GetBytes(messageAsString);

            Publish(channel, binaryMessage, exchange, routingKey);

            return channel;
        }

        public static IModel Publish(this IModel channel, byte[] message, string exchange, string routingKey)
        {
            var routingKeyValue = routingKey ?? string.Empty;

            channel.BasicPublish(exchange: exchange, routingKey: routingKeyValue, basicProperties: null, body: message);

            return channel;
        }
    }
}
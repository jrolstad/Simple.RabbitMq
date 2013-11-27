using RabbitMQ.Client;
using Simple.RabbitMq.UseCases.Messages;

namespace Simple.RabbitMq.UseCases.Publishing
{
    public class PublishSingleMessageToExchange
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using(var connectionFactory = new DisposableConnectionFactory())
            using(var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            {
                var message = new UsableMessage {Description = "Hello world!"};
                const string exchange = "TheUsableMessageExchange";

                channel.PublishAsJson(message, exchange);
            }
        }
    }
}
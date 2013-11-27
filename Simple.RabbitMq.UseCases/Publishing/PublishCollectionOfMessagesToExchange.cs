using Simple.RabbitMq.UseCases.Messages;

namespace Simple.RabbitMq.UseCases.Publishing
{
    public class PublishCollectionOfMessagesToExchange
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using(var connectionFactory = new DisposableConnectionFactory())
            using(var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            {
                var messages = new[]
                    {
                        new UsableMessage{Description = "Message1"},
                        new UsableMessage{Description = "Message2"},
                    };
                const string exchange = "TheUsableMessageExchange";

                channel.PublishCollectionAsJson(messages, exchange);
            }
        }
    }
}
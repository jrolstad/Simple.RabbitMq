using Simple.RabbitMq.UseCases.Messages;

namespace Simple.RabbitMq.UseCases.Creation
{
    public class CreateExchangeForType
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using (var connectionFactory = new DisposableConnectionFactory())
            using (var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare<UsableMessage>(RabbitMQ.Client.ExchangeType.Topic);
            }
        }
    }
}
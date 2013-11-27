using Simple.RabbitMq.UseCases.Messages;

namespace Simple.RabbitMq.UseCases.Creation
{
    public class CreateExchange
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using (var connectionFactory = new DisposableConnectionFactory())
            using (var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            {
                
                const string exchange = "TheUsableMessageExchange";

                channel.ExchangeDeclare(exchange,RabbitMQ.Client.ExchangeType.Topic);
            }
        }
    }
}
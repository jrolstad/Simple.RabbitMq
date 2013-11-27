namespace Simple.RabbitMq.UseCases.Creation
{
    public class CreateQueueAndBindToExchange
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using (var connectionFactory = new DisposableConnectionFactory())
            using (var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            {
                
                const string queueName = "TheUsableMessageQueue";
                const string exchange = "TheUsableMessageExchange";

                channel.ExchangeDeclare(exchange, RabbitMQ.Client.ExchangeType.Topic);
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true,arguments:null);

                channel.QueueBind(queue:queueName,exchange:exchange,routingKey:"");
            }
        }
    }
}
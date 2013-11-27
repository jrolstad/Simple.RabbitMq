namespace Simple.RabbitMq.UseCases.Creation
{
    public class CreateQueue
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using (var connectionFactory = new DisposableConnectionFactory())
            using (var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            {
                
                const string queueName = "TheUsableMessageQueue";

                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true,arguments:null);
            }
        }
    }
}
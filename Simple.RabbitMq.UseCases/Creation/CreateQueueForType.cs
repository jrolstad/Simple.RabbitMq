using Simple.RabbitMq.UseCases.Messages;

namespace Simple.RabbitMq.UseCases.Creation
{
    public class CreateQueueForType
    {
        public void Execute()
        {
            var url = Properties.Settings.Default.RabbitMqUrl;

            using (var connectionFactory = new DisposableConnectionFactory())
            using (var connection = connectionFactory.Create(url))
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare<UsableMessage>(isDurable: true, isExclusive: false, isAutoDelete: false);
            }
        }
    }
}
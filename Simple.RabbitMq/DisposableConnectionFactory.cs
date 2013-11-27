using System;
using RabbitMQ.Client;

namespace Simple.RabbitMq
{
    public class DisposableConnectionFactory:IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;

        public DisposableConnectionFactory():this(new ConnectionFactory())
        {
            
        }
        public DisposableConnectionFactory(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public virtual IConnection Create(string url)
        {
            _connectionFactory.Uri = url;

            return _connectionFactory.CreateConnection();
        }

        public void Dispose()
        {
            
        }
    }
}
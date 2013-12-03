using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Simple.RabbitMq
{
    public static class BasicDeliverEventArgsExtensions
    {
        public static T BodyAsJson<T>(this BasicDeliverEventArgs message)
        {
            var body = ConvertJsonToType<T>(message.Body);

            return body;
        }

        private static T ConvertJsonToType<T>(byte[] messageBody)
        {
            if (messageBody == null)
                return default(T);

            var bodyAsString = System.Text.Encoding.UTF8.GetString(messageBody);
            var bodyAsValue = JsonConvert.DeserializeObject<T>(bodyAsString);

            return bodyAsValue;
        }
    }
}
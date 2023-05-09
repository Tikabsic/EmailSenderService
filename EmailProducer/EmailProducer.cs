using EmailProducer.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace EmailProducer
{
    class EmailProducer
    {
        private readonly IModel _channel;
        private readonly string _queueName;

        public EmailProducer(IModel channel, string queueName)
        {
            _channel = channel;
            _queueName = queueName;

            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendEmail(EmailDTO emailMessage)
        {
            try
            {
                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emailMessage));

                _channel.BasicPublish(exchange: "",
                                      routingKey: _queueName,
                                      basicProperties: null,
                                      body: body);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
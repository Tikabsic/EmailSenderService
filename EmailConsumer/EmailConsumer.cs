using EmailProducer.DTO;
using EmailUtilities.SMTP;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
var emailSender = new EmailSender();
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "EmailSender", durable: true, exclusive: false, autoDelete: false, arguments: null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += async (model, eventargs) =>
    {
        var email = JsonConvert.DeserializeObject<EmailDTO>(Encoding.UTF8.GetString(eventargs.Body.ToArray()));

        await emailSender.SendEmailAsync(email.Recipient, email.Subject, email.Body);

        channel.BasicAck(eventargs.DeliveryTag, false);
    };

    channel.BasicConsume(queue: "EmailSender", autoAck: false, consumer: consumer);
}




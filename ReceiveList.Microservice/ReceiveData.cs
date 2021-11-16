using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiveList.Microservice
{
    class ReceiveData
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "Netanel_test", routingKey: "");
                Console.WriteLine("waiting");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    List<Person> sJSONResponse = JsonConvert.DeserializeObject<List<Person>>(message);
                    sJSONResponse.ForEach(i => Console.WriteLine(i));
                };
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

            }
        }
    }
}

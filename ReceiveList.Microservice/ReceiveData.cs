using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ReceiveList.Microservice
{
    class ReceiveData
    {

        static void Main(string[] args)
        {
            
            //Configure connection with RabbitMQ server
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Declare a Queue name and pull message from the sender by exchange
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "Netanel_test", routingKey: "");
                Console.WriteLine("waiting");

                //Get the data by consumer from the binding and Deserialize the list 
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    List<Person> sJSONResponse = JsonConvert.DeserializeObject<List<Person>>(message);
                    //Print on the console all the data from the sender
                    sJSONResponse.ForEach(i => Console.WriteLine(i));
                };

                //Start a Basic content class consumer
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

            }
        }
    }
}

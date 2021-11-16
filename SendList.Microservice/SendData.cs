using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendList.Microservice
{
    class SendData
    {

        static void Main(string[] args)
        {
            //Create a list of objects and insert them to the list
            var p = new Person(DateTime.Now, "Shuki Cohen", 32, "Football Player");
            var p2 = new Person(DateTime.Now, "Dani Cohen", 35, "Tennis Player");
            var p3 = new Person(DateTime.Now, "Shlomi Cohen", 38, "Barber");
            var peopleList = new List<Person>() { p, p2, p3 };

            //Covert list to JSON string to clean pass to RabbitMQ
            string sJSONResponse = JsonConvert.SerializeObject(peopleList);

            //Configure connection with RabbitMQ server
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Declare exchange name and Fanout option to brodcast every body the channel
                channel.ExchangeDeclare(exchange: "Netanel_test", type: ExchangeType.Fanout);
                string message = "test";
                //Show the data from Bytes to json string
                var body = Encoding.UTF8.GetBytes(sJSONResponse); 
                channel.BasicPublish(exchange: "Netanel_test", routingKey: "", basicProperties: null, body: body);
                Console.WriteLine("send {0}", message);
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

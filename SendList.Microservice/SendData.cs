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
            var p = new Person(DateTime.Now, "Shuki Cohen", 32, "Barber");
            var p2 = new Person(DateTime.Now, "Dani Cohen", 35, "Barber");
            var p3 = new Person(DateTime.Now, "Shlomi Cohen", 38, "Barber");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var list = new List<Person>() { p, p2, p3 };
            string sJSONResponse = JsonConvert.SerializeObject(list);

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "Netanel_test", type: ExchangeType.Fanout);
                string message = "test";
                var body = Encoding.UTF8.GetBytes(sJSONResponse);
                channel.BasicPublish(exchange: "Netanel_test", routingKey: "", basicProperties: null, body: body);
                Console.WriteLine("send {0}", message);
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

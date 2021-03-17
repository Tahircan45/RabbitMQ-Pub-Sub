using System;
using RabbitMQ.Client;
using System.Text;

namespace EmitLog
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
                    while (true)
                    {
                        Console.Write("Type Log: ");
                        var message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange:"logs",
                                             routingKey:"",
                                             basicProperties:null,
                                             body:body);
                        Console.WriteLine($"[X] Send {message}");
                    }
                }
            }
        }
    }
}

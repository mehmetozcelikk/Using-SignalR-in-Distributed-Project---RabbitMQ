using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailSenderExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection connectionSignalR = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/messagehub").Build();
            await  connectionSignalR.StartAsync();

            ConnectionFactory factory = new ConnectionFactory();

            factory.Uri = new Uri("AMQP URL required");

            using IConnection connection = factory.CreateConnection();
            using IModel chanel = connection.CreateModel();
            chanel.QueueDeclare("messagequeue", false, false, false);


            EventingBasicConsumer consumer = new EventingBasicConsumer(chanel);
            chanel.BasicConsume("messagequeue", true, consumer);

            consumer.Received += async (s, e) =>
            {
                string seriliazeData = Encoding.UTF8.GetString(e.Body.Span);
                User user = JsonSerializer.Deserialize<User>(seriliazeData);
                EmailSender.Send(user.Email, user.Message);
                Console.WriteLine($"{user.Email} e-mail has been sent.");

                
                await connectionSignalR.InvokeAsync("SendMessageAsync", $"{user.Email} e-mail has been sent.");
            };
            Console.Read();
        }      
    }
}

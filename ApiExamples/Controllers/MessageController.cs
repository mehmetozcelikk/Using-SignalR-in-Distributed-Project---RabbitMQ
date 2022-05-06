using ApiExamples.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace ApiExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Post([FromForm]User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("AMQP URL required");
            using IConnection connection = factory.CreateConnection();
            using IModel chanel = connection.CreateModel();
            chanel.QueueDeclare("messagequeue" , false,false,false);
            string serializeData = JsonSerializer.Serialize(model); 
            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            chanel.BasicPublish("", "messagequeue", body: data);
            return Ok();
        }
    }
}

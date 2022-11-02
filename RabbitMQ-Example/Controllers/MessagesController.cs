using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ_Example.Model;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace RabbitMQ_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        [HttpPost()]
        public IActionResult Post([FromForm]User model)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://reyejeoz:.......");//ilgili RabbitMQ adresine baglandik.
            using IConnection connection = factory.CreateConnection();

            using IModel channel = connection.CreateModel(); //Kanal olusturduk.

            channel.QueueDeclare("messagequeue",true,false); //ilk parametre: kuyruga bir isim veriyoruz. İkinci parametre: Sunucu da olasi bir hatada kapanma/dispose gibi durumlarda, sunucu tekrar calıstıgında mesajlarlar hala gecerli olsun mu olmasin mi diye. Ucuncu Parametre: Bu kuyruga birden fazla kanalin baglanıp baglanamama durumunu kontrol ediyor. dorduncu parametre: tum mesajlar bitince kuyrugu imha etsin mi?

            string serializeData =JsonSerializer.Serialize(model);
            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            channel.BasicPublish("", "messagequeue",body:data);


            return Ok();
        }
    }
}

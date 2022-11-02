using EmailSenderExample;
using EmailSenderExample.Contracts;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

HubConnection connectionSignalR = new HubConnectionBuilder().WithUrl("https://localhost:7213/messagehub").Build();
await connectionSignalR.StartAsync();
ConnectionFactory factory = new();
factory.Uri = new("amqps://reyejeoz:.......");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel(); 

channel.QueueDeclare("messagequeue", true, false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("messagequeue", true,consumer); //Hangi kuyrugu dinleyeceksek onu yazıyoruz.


consumer.Received += async (s, e) => /*kuyruga gelen ilgili mesajlar burada yakalanmış olacak */
{
    //mail operasyonları burada gerceklesecek.
    //e.body.span

    string serializeData = Encoding.UTF8.GetString(e.Body.Span); //Byte olan datayı stringe çeviriyoruz.
    User user =  JsonSerializer.Deserialize<User>(serializeData); //Serialize yaptığımız datayı, DESERİALİZE yaparak formatı uygun hale getirip "User" Tipinde bir tip guvenligi sagladik. Contract olarak tanımladığımız User tipi..

    EmailSender.Send(user.Email,user.message);

    Console.WriteLine($"{user.Email} adresine Mail Gönderildi.");
    await connectionSignalR.InvokeAsync("SendMessageAsync", $"{user.Email} adresine Mail Gönderildi.");
    
    
};
Console.Read();
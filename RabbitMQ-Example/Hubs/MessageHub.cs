using Microsoft.AspNetCore.SignalR;

namespace RabbitMQ_Example.Hubs
{
    public class MessageHub:Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage",message); //Mevcut ne kadar client varsa mesajı gönderiyoruz. Mesaja subscribe olan clientlar  

        }
    }
}

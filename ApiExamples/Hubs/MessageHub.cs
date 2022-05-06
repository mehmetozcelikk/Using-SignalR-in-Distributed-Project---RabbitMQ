using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ApiExamples.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}

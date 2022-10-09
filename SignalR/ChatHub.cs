using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Models;

namespace SignalRMVC.SignalR
{
    public class ChatHub : Hub
    {
        ApplicationContext db;
        public ChatHub(ApplicationContext context)
        {
            db = context;
        }
        public async Task Send(string message, string userName)
        {
            Message message1 = new Message { UserName = userName, MessageText = message };
            await db.Messages.AddAsync(message1);
            await db.SaveChangesAsync();
            await Clients.All.SendAsync("Receive", message, userName);
        }
    }
}

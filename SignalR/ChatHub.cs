using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Models;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace SignalRMVC.SignalR
{
    public class ChatHub : Hub
    {
        ApplicationContext db;
        UserManager<User> _userManager;
        public ChatHub(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            db = context;
        }
        public async Task Send(string message, string userName, string receiverName)
        {
            Message message1 = new Message { ReceiverName = receiverName, UserName = userName, MessageText = message };
            await db.Messages.AddAsync(message1);
            await db.SaveChangesAsync();

            User user = await _userManager.FindByNameAsync(receiverName);
            string receiver = user.Id;

            //await Clients.All.SendAsync("Receive", message, userName);
            await Clients.Users(Context.UserIdentifier, receiver).SendAsync("Receive", message, userName);

        }
    }
}

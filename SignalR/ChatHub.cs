using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Models;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;
using System;

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
            DateTime date1 = DateTime.Now;
            string date = date1.ToString("dd.MM.yyyy HH:mm");
            Message message1 = new Message { ReceiverName = receiverName, UserName = userName, MessageText = message , Date = date};
            await db.Messages.AddAsync(message1);
            await db.SaveChangesAsync();

            User user = await _userManager.FindByNameAsync(receiverName);
            string receiver = user.Id;

            //await Clients.All.SendAsync("Receive", message, userName);
            await Clients.Users(Context.UserIdentifier, receiver).SendAsync("Receive", message, userName, date);

        }
    }
}

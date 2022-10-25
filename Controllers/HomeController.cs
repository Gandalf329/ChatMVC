using Microsoft.AspNetCore.Mvc;
using SignalRMVC.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SignalRMVC.Controllers
{
    public class HomeController : Controller
    {

        ApplicationContext db;
        UserManager<User> _userManager;
        public HomeController(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> DefaultMessage()
        {
            return View();
        }
        public async Task<IActionResult> ViewUserMessages(string name, string name2)
        {
            User user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                //Console.WriteLine("user1 null");
                return NotFound();
            }
            User user2 = await _userManager.FindByNameAsync(name2);
            if (user2 == null)
            {
                //Console.WriteLine("user1 null");
                return NotFound();
            }
            var messages = from m in db.Messages select m;
            if (!String.IsNullOrEmpty(name) || !String.IsNullOrEmpty(name2))
            {
                messages = messages.Where(s => (s.UserName.Contains(name) || s.ReceiverName.Contains(name)) && (s.UserName.Contains(name2) || s.ReceiverName.Contains(name2)));
            }

            return View(await messages.ToListAsync());
        }
        public async Task<IActionResult> AllUsers()
        {
            return View(await _userManager.Users.ToListAsync());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
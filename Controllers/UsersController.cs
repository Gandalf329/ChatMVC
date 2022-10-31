using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalRMVC.Models;

namespace SignalRMVC.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> UserProfile(string name)
        {
            User user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPut]
        public async Task<IActionResult> EditProfile(string name)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{name}");
            Console.WriteLine("Another line.");
            Console.ResetColor();
            User user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            
            user.UserName = name;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}

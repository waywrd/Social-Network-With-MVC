using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Share.Services;
using Share.Web.Hubs;
using Share.Web.Models;

namespace Share.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHubContext<ChatHub> chatHub;

        private IUserService userService; 
        public HomeController(IUserService userService,IHubContext<ChatHub> chatHub)
        {
            this.userService = userService;
            this.chatHub = chatHub;
        }
        public IActionResult Index()
        {
            var users = this.userService.GetAllUsers();
            var posts = this.userService.GetPosts();
            var indexViewModel = new IndexViewModel
            {
                Posts = posts,
                Users = users
            };
            
            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("[controller]/[action]")]
        [Route("[controller]/[action]/{id}")]
        public IActionResult ChatWindow()
        {
            var users = this.userService.GetAllUsers();
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

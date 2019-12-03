using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Share.Data.Models;
using Share.Data.Models.Enums;
using Share.Services;
using Share.Web.Models;

namespace Share.Web.Controllers
{
    public class UserController : Controller


    {
        private IHostingEnvironment hostingEnvironment;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            this.userManager = userManager;
            this.userService = userService;
            hostingEnvironment = environment;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendRequest(string receiverId)
        {

            var username = this.HttpContext.User.Identity.Name;


            await this.userService.SendRequest(username, receiverId);

            return Redirect("/");
        }

        public IActionResult FriendRequests()
        {
            var name = this.User.Identity.Name;
            var requests = userService.GetRequests(name);
            return View(requests);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AcceptFriendRequest(string friendId)
        {

            var username = this.HttpContext.User.Identity.Name;


            await this.userService.AcceptFriendRequest(username, friendId);

            return Redirect("/");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserPost(string title, string content, IFormFile image)
        {

            var userId = this.userManager.GetUserId(this.HttpContext.User);

            if (image != null && image.Length > 0)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var saveImagePath = Path.Combine(uploads, image.FileName);
                var appImagePath = String.Concat("/uploads/", image.FileName);


                using (var fileStream = new FileStream(saveImagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);

                }
                await this.userService.UserPost(title, content, userId, appImagePath);
            }

            return Redirect("/");
        }

        
        [Authorize]
        public IActionResult CreateGroup()
        {
            var group = new Group();

            return View(group);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGroup(string groupName, string groupDescription, GroupStatusEnum groupStatusEnum)
        {
            var userName = this.HttpContext.User.Identity.Name;
            var user = await this.userManager.FindByNameAsync(userName);


            this.userService.CreatGroup(groupName, groupDescription, user, groupStatusEnum);

            return Redirect("/");
        }

        [Authorize]
        public IActionResult GetAllPublicGroups()
        {
            var groups = this.userService.GetAllPublicGroups();
            return View(groups);
        }

        [Authorize]
        public IActionResult GetGroup(string groupName)
        {
            var groupViewModel = new GetAllGroupsViewModel
            {
                Groups = this.userService.GetGroup(groupName),
                GroupPosts = this.userService.GetGroupPosts(groupName)
            };
            return View(groupViewModel);
        }

        [Authorize]
        [HttpPost]
       public async Task<IActionResult> GroupPost(string title,string content,IFormFile image,int id)
        {

            var userId = this.userManager.GetUserId(this.HttpContext.User);

            if (image != null && image.Length > 0)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads/groups");
                var saveImagePath = Path.Combine(uploads, image.FileName);
                var appImagePath = String.Concat("/uploads/groups/", image.FileName);


                using (var fileStream = new FileStream(saveImagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);

                }
                await this.userService.GroupPost(title, content, userId, appImagePath,id);
            }

            return Redirect("/");
        }

    }
}
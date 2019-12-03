using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Share.Data;
using Share.Data.Models;
using Share.Data.Models.Enums;
using Share.Data.ViewModels;

namespace Share.Services
{
    public class UserService : IUserService
    {
        private readonly ShareDbContext db;
        private readonly UserManager<ApplicationUser> userManager;


        public UserService(ShareDbContext db,UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task AcceptFriendRequest(string username, string receiverId)
        {
            var user=await userManager.FindByNameAsync(username);

            var areFriendsEntity = db.Friends.Where(p => p.ApplicationUserId == user.Id && p.ApplicationUserFriendId == receiverId).FirstOrDefault();
            var otherUserFriendEntity = db.Friends.Where(p => p.ApplicationUserId == receiverId && p.ApplicationUserFriendId == user.Id).FirstOrDefault();
            areFriendsEntity.FriendshipStatus = FriendshipStatus.Friends;
            otherUserFriendEntity.FriendshipStatus = FriendshipStatus.Friends;
            areFriendsEntity.FriendshipDate = DateTime.UtcNow;
            otherUserFriendEntity.FriendshipDate = DateTime.UtcNow;
            db.SaveChanges();
        }

       

        public  void CreatGroup(string groupName, string groupDescription,ApplicationUser user, GroupStatusEnum groupStatusEnum)
        {
            var group= new Group
            {
                GroupName = groupName,
                GroupDescription = groupDescription,
                Owner = user,
                GroupStatusEnum=groupStatusEnum
            };

            db.Groups.Add(group);

            db.SaveChanges();
        }

        public ICollection<Group> GetAllPublicGroups()
        {
            return db.Groups.Where(p => p.GroupStatusEnum==GroupStatusEnum.Public).ToList();
        }

        public ICollection<ApplicationUser> GetAllUsers()
        {
            return db.Users.Include(p=>p.FriendList).ToList();
        }

        public Group GetGroup(string groupName)
        {
            return db.Groups.Where(p => p.GroupName == groupName).FirstOrDefault();
        }

        public ICollection<GroupPost> GetGroupPosts(string groupName)
        {
            return db.GroupPosts.Where(p => p.Group.GroupName == groupName).Include(p=>p.ApplicationUser).OrderByDescending(p => p.CreatedOn).ToList();
        }

        public ICollection<Post> GetPosts()
        {
            return db.Posts.Where(p=>p.GetType().Name.Equals("Post")).OrderByDescending(p=>p.CreatedOn).ToList(); ;
        }

        public ICollection<FriendRequestViewModel> GetRequests(string username)
        {
            var friendRequests = db.Friends.Where(p => p.ApplicationUser.UserName == username && p.FriendshipStatus==FriendshipStatus.OtherPending).Include(p=>p.ApplicationUserFriend).Select(p =>
             new FriendRequestViewModel { UserId = p.ApplicationUserFriendId, Username = p.ApplicationUserFriend.UserName }).ToList();
            return friendRequests;
        }

        public async Task GroupPost(string title, string content, string userId, string appImagePath, int groupId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var group = db.Groups.Where(p => p.Id == groupId).FirstOrDefault();

            var post = new GroupPost
            {
                Title = title,
                Content = content,
                CreatedOn = DateTime.UtcNow,
                ImagePath = appImagePath,
                ApplicationUser = user,
                Group=group
                
            };
            db.GroupPosts.Add(post);
            db.SaveChanges();
        }

        public async Task SendRequest(string username,string receiverId)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var receiver = await this.userManager.FindByIdAsync(receiverId);

            var friend1 = new Friend { ApplicationUser = user, ApplicationUserFriend = receiver,FriendshipStatus=FriendshipStatus.UserPending };
            var friend2= new Friend { ApplicationUser = receiver, ApplicationUserFriend = user,FriendshipStatus=FriendshipStatus.OtherPending };

            user.FriendList.Add(friend1);

            receiver.FriendList.Add(friend2);

            await db.SaveChangesAsync();
        }

        public async Task UserPost(string title, string content, string applicationUserId, string image)
        {
            var user = await userManager.FindByIdAsync(applicationUserId);

            var post = new Post
            {
                Title = title,
                Content = content,
                CreatedOn = DateTime.UtcNow,
                ImagePath = image,
                ApplicationUser = user,
            };
            db.Posts.Add(post);
            db.SaveChanges();
        }

       
    }
}

using Share.Data.Models;
using Share.Data.Models.Enums;
using Share.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Share.Services
{
    public interface IUserService
    {
        ICollection<ApplicationUser> GetAllUsers();

        Task SendRequest(string senderId, string username);

        ICollection<FriendRequestViewModel> GetRequests(string username);
        Task AcceptFriendRequest(string username, string receiverId);

        Task UserPost(string title,string content, string applicationUserId,string image);
        ICollection<Post> GetPosts();
        void CreatGroup(string groupName, string groupDescription, ApplicationUser user,GroupStatusEnum groupStatusEnum);
        ICollection<Group> GetAllPublicGroups();
        Group GetGroup(string groupName);
        Task GroupPost(string title, string content, string userId, string appImagePath,int groupId);
        ICollection<GroupPost> GetGroupPosts(string groupName);
    }
}

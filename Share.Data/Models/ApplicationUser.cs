using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Share.Data.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Friend> FriendList { get; set; } = new List<Friend>();

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<GroupMembers> Groups { get; set; }

        public string ProfilePictureImagePath { get; set; }

    }
}

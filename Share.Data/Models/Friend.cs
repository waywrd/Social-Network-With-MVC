using System;

namespace Share.Data.Models
{
    public class Friend
    {
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationUser ApplicationUserFriend { get; set; }

        public string ApplicationUserId { get; set; }
        public string ApplicationUserFriendId { get; set; }


        public  FriendshipStatus FriendshipStatus { get;set; }
        public DateTime FriendshipDate { get; set; }
    }
}

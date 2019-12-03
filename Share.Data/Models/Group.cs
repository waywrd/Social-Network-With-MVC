using Share.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Data.Models
{
    public class Group
    {

        public int Id { get; set; }
        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public ApplicationUser Owner { get; set; }

        public string OwnerId { get; set; }

        public GroupStatusEnum GroupStatusEnum { get; set; }


        public ICollection<GroupPost> GroupPosts { get; set; }

        public ICollection<GroupMembers> Members { get; set; }

        public string CoverPhotoImagePath { get; set; }
    }
}

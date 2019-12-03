using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Data.Models
{
    public class GroupPost:Post
    {
        public Group Group { get; set; }

        public int GroupId { get; set; }
    }
}

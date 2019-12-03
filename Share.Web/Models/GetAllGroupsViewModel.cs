using Share.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.Web.Models
{
    public class GetAllGroupsViewModel
    {

        public Group Groups { get; set; }

        public ICollection<GroupPost> GroupPosts { get; set; }
    }
}

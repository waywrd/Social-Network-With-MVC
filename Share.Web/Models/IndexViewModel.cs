using Share.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.Web.Models
{
    public class IndexViewModel
    {

        public ICollection<Post> Posts { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}

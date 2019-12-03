using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

       
        public DateTime CreatedOn { get; set; }

        public string ImagePath { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Data.Models
{
   public class GroupMembers
    {
      
        public ApplicationUser Member { get; set; }

        public Group Group { get; set; }

        
        public string MemberId { get; set; }

        public int GroupId { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Post
    {
        public string Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string Content { get; set; }
    }
}

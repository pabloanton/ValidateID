using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public List<Post> Posts { get; set; }
    }
}

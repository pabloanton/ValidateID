using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class ProductQuanty
    {
        [Key]
        public int id { get; set; }
        public Product product { get; set; }
        public int quanty { get; set; }
    }
}

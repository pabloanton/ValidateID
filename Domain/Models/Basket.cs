using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Basket
    {
        [Key]
        public int id { get; set; }
        public List<ProductQuanty> products { get; set; }
        public User user { get; set; }
        public DateTime creationDate { get; set; }


    }
}

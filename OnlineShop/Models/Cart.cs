using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public IdentityUser Customer { get; set; }
        public bool isOrdered { get; set; }
    }
}

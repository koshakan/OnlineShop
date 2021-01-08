using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class CartItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public Cart Cart { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class CreateOrderViewModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

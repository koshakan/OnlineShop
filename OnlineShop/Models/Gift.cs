using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Gift: Product
    {
        [Required]
        public string GiftName { get; set; }
        [Required]
        public string GiftDescription { get; set; }
        [Required]
        public double GiftPrice { get; set; }
        
    }
}

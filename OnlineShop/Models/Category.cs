using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Category")]
        [Required]
        public string Name { get; set; }
    }
}

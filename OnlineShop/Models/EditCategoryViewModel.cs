using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class EditCategoryViewModel
    {

        public EditCategoryViewModel()
        {

        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
    }
}

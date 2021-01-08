using System.ComponentModel.DataAnnotations;

namespace Square.Models
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; } 
    }
}
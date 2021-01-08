using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class EditGiftViewModel
    {

        public EditGiftViewModel()
        {

        }
        public int Id { get; set; }
        [ValidateNever]
        public string OldImage { get; set; }
    }
}

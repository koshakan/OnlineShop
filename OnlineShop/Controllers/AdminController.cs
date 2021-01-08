using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Identity.UI.Services;
using OnlineShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGiftRepository _giftRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IEmailSender _emailSender;
        [Obsolete]
        private static IHostingEnvironment _appEnvironment;

        [Obsolete]
        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ICategoryRepository categoryRepository, 
            IGiftRepository giftRepository,
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IEmailSender emailSender,
            IHostingEnvironment appEnvironment)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _giftRepository = giftRepository;
            _appEnvironment = appEnvironment;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _emailSender = emailSender;
        }

        [Obsolete]
        private static string UploadImageGift(CreateGiftViewModel model)
        {
            var uploadFolder = Path.Combine(_appEnvironment.ContentRootPath + "\\wwwroot\\img\\wheel");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            var path = Path.Combine(uploadFolder, uniqueFileName);
            var fileStream = new FileStream(path, FileMode.Create);
            model.Image.CopyTo(fileStream);
            fileStream.Close();
            return uniqueFileName;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListOrders()
        {
            var orders = _orderRepository.GetAllOrder();
            foreach (var order in orders)
            {
                order.Customer = _userManager.FindByIdAsync(order.CustomerId).Result;
            }

            return View(orders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            var order = _orderRepository.GetOrder(id);
            var cartItems = _cartItemRepository.GetAllCartItems().Where(c => c.CartId == order.CartId);
            var total = 0.0;
            foreach (var cartItem in cartItems)
            {
                var product = _giftRepository.GetGift(cartItem.ProductId);
                cartItem.Product = product;
                total += product.Price;
            }

            order.Customer = _userManager.FindByIdAsync(order.CustomerId).Result;
            ViewBag.Order = order;
            ViewBag.Products = cartItems;
            ViewBag.Total = total;
            return order == null ? View("NotFound") : View();
        }

        [HttpGet]
        public IActionResult AcceptOrder(int id)
        {
            var order = _orderRepository.GetOrder(id);
            if (order != null)
            {
                order.IsAccepted = true;
                _orderRepository.Update(order);
                var user = _userManager.FindByIdAsync(order.CustomerId).Result;
                var email = user.Email;
                var subject = order.Id.ToString();
                var message = "Dear " + user.UserName + ". Your order: " + order.Id +
                              " is confirmed. Our manager will contact you very soon";
                _emailSender.SendEmailAsync(email, subject, message);
                return RedirectToAction("ListOrders");
            }
            ViewBag.ErrorMessage = $"Order cannot be found";
            return View("NotFound");

        }

        [HttpGet]
        public IActionResult DeleteOrder(int id)
        {
            var order = _orderRepository.Delete(id);
            if (order != null) return RedirectToAction("ListOrders");
            ViewBag.ErrorMessage = $"Order cannot be found";
            return View("NotFound");

        }
    }
}

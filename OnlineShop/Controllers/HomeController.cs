using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Models;
using OnlineShop.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGiftRepository _giftRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger,
            IGiftRepository giftRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IOrderRepository orderRepository,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _giftRepository = giftRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GiftDetails(int id)
        {
            ViewBag.Gift = _giftRepository.GetGift(id);
            return View();
        }

        [Authorize]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MakeOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MakeOrder(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = GetCurrentUserAsync().Result;
                var cart = _cartRepository.GetAllCarts().Last(c => c.CustomerId == user.Id && c.isOrdered == false);
                var order = new Order()
                {
                    Customer = user,
                    Cart = cart,
                    CustomerId = user.Id,
                    CartId = cart.Id,
                    Address = model.Address,
                    Phone = model.Phone
                };
                _orderRepository.Add(order);
                cart.isOrdered = true;
                _cartRepository.Update(cart);
            }

            return RedirectToAction("Index");
        }
    }
    
}

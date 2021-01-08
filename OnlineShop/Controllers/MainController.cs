using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
        [ApiController]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]")]
        public class MainController : ControllerBase
        {
            private readonly IGiftRepository _giftRepository;
            private readonly ICartRepository _cartRepository;
            private readonly ICartItemRepository _cartItemRepository;
            private readonly UserManager<IdentityUser> _userManager;

        public MainController(
                IGiftRepository giftRepository,
                ICartRepository cartRepository,
                ICartItemRepository cartItemRepository,
                UserManager<IdentityUser> userManager
                )
            {
                _giftRepository = giftRepository;
                _cartRepository = cartRepository;
                _cartItemRepository = cartItemRepository;
                _userManager = userManager;
            }

            private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

            [HttpPost("addtocart")]
            public CartItem AddToCart(JsonElement request)
            {
                var productId = request.GetProperty("productId").GetString();
                var categoryId = request.GetProperty("categoryId").GetString();
                Product product;
                product = _giftRepository.GetGift(int.Parse(productId));

                var user = GetCurrentUserAsync().Result;
                var cart = _cartRepository.GetAllCarts().LastOrDefault(c => c.CustomerId == user.Id && c.isOrdered == false) ?? new Cart();
                cart.Customer = user;
                cart.CustomerId = user.Id;
                var cartItem = _cartItemRepository.GetAllCartItems()
                                   .FirstOrDefault(c => c.ProductId == product.Id && c.CartId == cart.Id) ?? new CartItem();
                cartItem.Product = product;
                cartItem.Cart = cart;
                cartItem.Quantity++;
                cartItem.CartId = cart.Id;
                cartItem.ProductId = product.Id;
                _cartItemRepository.AddOrUpdate(cartItem);
                _cartRepository.AddOrUpdate(cart);

                return cartItem;

            }

            [HttpGet("cart")]
            public IEnumerable<CartItem> GetCart()
            {
                var user = GetCurrentUserAsync().Result;
                var cart = _cartRepository.GetAllCarts().LastOrDefault(c => c.CustomerId == user.Id && c.isOrdered == false) ?? new Cart();
                var cartItems = _cartItemRepository.GetAllCartItems().Where(c => c.CartId == cart.Id);
                foreach (var cartItem in cartItems)
                {
                var product = _giftRepository.GetGift(cartItem.ProductId);
                    cartItem.Product = product;
                }
                _cartRepository.AddOrUpdate(cart);

                return cartItems;
            }

            [HttpDelete("cartitem/{cartItemId}")]
            public IActionResult DeleteCartItem(int cartItemId)
            {
                var result = _cartItemRepository.Delete(cartItemId);
                if (result == null) return BadRequest("Item not found");
                return Ok();
            }
        }
}

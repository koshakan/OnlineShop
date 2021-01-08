using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Repositories
{
    public interface ICartItemRepository
    {
        CartItem GetCartItem(int id);
        IEnumerable<CartItem> GetAllCartItems();
        CartItem Add(CartItem cartItem);
        CartItem Delete(int id);
        CartItem Update(CartItem cartItem);

        void AddOrUpdate(CartItem cartItem);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public interface ICartRepository
    {
        Cart GetCart(int id);
        IEnumerable<Cart> GetAllCarts();
        Cart Add(Cart cart);
        Cart Delete(int id);
        Cart Update(Cart cart);

        void AddOrUpdate(Cart cart);
    }
}

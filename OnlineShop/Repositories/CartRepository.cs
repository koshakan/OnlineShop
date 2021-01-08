using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Cart GetCart(int id)
        {
            return _context.Carts.Find(id);
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            return _context.Carts;
        }

        public Cart Add(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart;
        }

        public Cart Delete(int id)
        {
            var cart = _context.Carts.Find(id);
            if (cart == null) return null;
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return cart;
        }

        public Cart Update(Cart cart)
        {
            var c = _context.Carts.Attach(cart);
            c.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return cart;
        }

        public void AddOrUpdate(Cart cart)
        {
            var entry = _context.Entry(cart);
            switch (entry.State)
            {
                case EntityState.Detached:
                    Add(cart);
                    break;
                case EntityState.Modified:
                    Update(cart);
                    break;
                case EntityState.Added:
                    Add(cart);
                    break;
                case EntityState.Unchanged:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public class GiftRepository
    {
        private readonly ApplicationDbContext _context;

        public GiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Gift GetGift(int id)
        {
            return _context.Gifts.Find(id);
        }

        public IEnumerable<Gift> GetAllTires()
        {
            return _context.Gifts;
        }

        public Gift Add(Gift gift)
        {
            _context.Gifts.Add(gift);
            _context.SaveChanges();
            return gift;
        }

        public Gift Delete(int id)
        {
            var gift = _context.Gifts.Find(id);
            if (gift == null) return null;
            _context.Gifts.Remove(gift);
            _context.SaveChanges();
            return gift;
        }

        public Gift Update(Gift gift)
        {
            var g = _context.Gifts.Attach(gift);
            g.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return gift;
        }
    }
}

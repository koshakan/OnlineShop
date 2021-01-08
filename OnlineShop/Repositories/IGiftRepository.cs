using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public interface IGiftRepository
    {
        Gift GetGift(int id);
        IEnumerable<Gift> GetAllGifts();
        Gift Add(Gift gift);
        Gift Delete(Gift gift);
        Gift Update(Gift gift);
    }
}

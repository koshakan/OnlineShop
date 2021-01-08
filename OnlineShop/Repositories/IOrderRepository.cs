using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        IEnumerable<Order> GetAllOrder();
        Order Add(Order order);
        Order Update(Order order);
        Order Delete(int id);
    }
}

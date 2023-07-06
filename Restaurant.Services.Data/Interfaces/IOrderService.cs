using Restaurant.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Data.Interfaces
{
    public interface IOrderService
    {

        Task AddOrderAsync(AddOrderViewModel model, List<int> cartItems, Guid userId);

        Task<IEnumerable<OrderViewModel>> AllOrdersAcync();
    }
}

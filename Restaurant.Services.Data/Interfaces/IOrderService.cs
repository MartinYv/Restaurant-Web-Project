using Restaurant.Data.Models;
using Restaurant.Services.Data.Models.Order;
using Restaurant.ViewModels.Models.Order;
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
        Task<IEnumerable<OrderViewModel>> AllOrdersAcync();
        Task<AllOrdersFilteredServiceModel> UserOrdersAsync(AllOrdersQueryViewModel queryModel);
            string? GetUserId();

        Task<AllOrdersFilteredServiceModel> AllFilteredAsync(AllOrdersQueryViewModel queryModel);

        Task<IEnumerable<string>> AllCategoryNamesAsync();
        Task<Order?> FindOrderByIdAsync(int orderId);
        Task ChangeStatusByIdAsync(int orderId);
    }
}

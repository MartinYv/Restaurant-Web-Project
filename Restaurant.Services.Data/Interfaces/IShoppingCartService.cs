using Restaurant.Data.Models;
using Restaurant.ViewModels.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant.Common.EntityValidationConstants;

namespace Restaurant.Services.Data.Interfaces
{
    public interface IShoppingCartService
    {
        Task<int> AddItem(int bookId, int qty);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart?> GetCart(string userId);
        Task<bool> DoCheckout(OrderUsersInfoViewModel usersInfo);
    }

}

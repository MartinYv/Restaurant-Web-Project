using Restaurant.Data.Models;
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
        void AddToCart(string cartId, int dishId, int quantity,int? orderId);
        void RemoveFromCart(string cartId, int cartItemId);
        void UpdateCartItemQuantity(string cartId, int cartItemId, int quantity);
        void ClearCart(string cartId);
        Cart GetCart(string cartId);
    }

}

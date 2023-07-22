using Restaurant.ViewModels.Models.Order;

namespace Restaurant.Services.Data.Models.Order
{
    public class AllOrdersFilteredServiceModel
    {
        public int TotalOrdersCount { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}
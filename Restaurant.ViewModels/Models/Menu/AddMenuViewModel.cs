using Restaurant.Data.Models;


namespace Restaurant.ViewModels.Models.Menu
{

    public class AddMenuViewModel
    {
		public int Id { get; set; }
		public IEnumerable<MenuType> MenuTypes { get; set; } = new HashSet<MenuType>();

	}
}

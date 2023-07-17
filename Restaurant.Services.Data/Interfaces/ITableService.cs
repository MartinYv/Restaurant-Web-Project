using Restaurant.ViewModels.Models.Table;

namespace Restaurant.Services.Data.Interfaces
{
	public interface ITableService
	{
		Task AddTableAsync(AddTableViewModel model);
		Task<IEnumerable<AllTablesViewModel>> AllTablesAsync();
		Task DeleteTableById(int id);
	}
}

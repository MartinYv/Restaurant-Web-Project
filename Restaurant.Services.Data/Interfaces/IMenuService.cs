using Restaurant.Data.Models;
using Restaurant.ViewModels.Models.Menu;


namespace Restaurant.Services.Data.Interfaces
{
    public interface IMenuService
    {
        Task AddMenuAcync(AddMenuViewModel model);
        Task<IEnumerable<MenuType>> GetAllMenuTypesAsync();

        Task <IEnumerable<AllMenusViewModel>> AllMenusAsync();
        Task DeleteMenuAsync(int menuId);
    }
}

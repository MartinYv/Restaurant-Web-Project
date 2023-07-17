using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Table;
using Restaurant2.Data;

namespace Restaurant.Services.Data
{
	public class TableService : ITableService
	{
		private readonly RestaurantDbContext context;

        public TableService(RestaurantDbContext _context)
        {
			context = _context;
        }
        public async Task AddTableAsync(AddTableViewModel model)
		{
			if (context.Tables.Where(t=>t.IsDeleted == false).Any(t=>t.Number == model.Number))
			{
				throw new ArgumentException("Table with that number is already created.");
			}

			Table table = new Table()
			{
				Number = model.Number,
				Seats = model.Seats
			};

			await context.AddAsync(table);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AllTablesViewModel>> AllTablesAsync()
		{
			return await context.Tables.Where(t => t.IsDeleted == false)
				.Select(t => new AllTablesViewModel()
				{
					Id = t.Id,
					Number = t.Number,
					Seats = t.Seats
				}).ToListAsync();
		}

		public async Task DeleteTableById(int id)
		{
			Table? table = await context.Tables.FindAsync(id);

			if (table == null)
			{
				throw new ArgumentException("Invalid table id.");
			}

			table.IsDeleted = true;
			await context.SaveChangesAsync();
		}
	}
}

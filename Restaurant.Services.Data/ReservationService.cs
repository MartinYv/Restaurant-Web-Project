using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Reservation;
using Restaurant.Data;
using System.Security.Claims;

namespace Restaurant.Services.Data
{
	public class ReservationService : IReservationService
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly RestaurantDbContext context;

		public ReservationService(IHttpContextAccessor _httpContextAccessor, RestaurantDbContext _context)
		{
			httpContextAccessor = _httpContextAccessor;
			context = _context;
		}

		public async Task AddReservationAsync(AddReservationInfoViewModel model)
		{
			var userId = GetUserId();
			if (userId == null)
			{
				throw new ArgumentException("You have to log in.");
			}

			var user = await context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(GetUserId()!));

			if (user == null)
			{
				throw new ArgumentException("Invalid user id.");
			}


			Reservation reservation = new Reservation()
			{
				Customer = user,
				CustomerId = user.Id,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Phone = model.Phone,
				Persons = model.Persons,
				//Hour = model.Hour,
			};

			await context.Reservations.AddAsync(reservation);
			await context.SaveChangesAsync();
		}


		public async Task<IEnumerable<AllReservationsViewModel>> AllReservationsAsync()
		{
			return await context.Reservations.Where(r => r.IsDeleted == false).
				Select(r => new AllReservationsViewModel()
				{
					FirstName = r.FirstName,
					LastName = r.LastName,
				    Hour = r.Hour.ToString(),
					Phone = r.Phone,
					Persons = r.Persons,
					TableNumber = r.Table.Number,
					TableSeats = r.Table.Seats
				}).ToListAsync();
		}

		public Task DeleteReservationAsync(int id)
		{
			throw new NotImplementedException();
		}

		public string? GetUserId()
		{
			string? userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
			return userId;
		}

		public Task<Reservation> UserReservationAsync(Guid userId)
		{
			throw new NotImplementedException();
		}
	}
}

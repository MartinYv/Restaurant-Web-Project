using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data.Interfaces;
using Restaurant.ViewModels.Models.Reservation;
using Restaurant2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public async Task AddReservationAsync(AddReservationViewModel model)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

			if (user == null)
			{
				throw new ArgumentException("Invalid user id.");
			}

			// after the AddReservationView, need to chack in tableService for propper table according the needed seats // TODO
		}

		public Task<IEnumerable<AllReservationsViewModel>> AllReservationsAsync()
		{
			throw new NotImplementedException();
		}

		public Task DeleteReservationAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Guid GetUserId()
		{
			Guid userId = Guid.Parse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
			return userId;
		}

		public Task<Reservation> UserReservationAsync(Guid userId)
		{
			throw new NotImplementedException();
		}
	}
}

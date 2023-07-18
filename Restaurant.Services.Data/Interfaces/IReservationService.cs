﻿using Restaurant.Data.Models;
using Restaurant.ViewModels.Models.Reservation;

namespace Restaurant.Services.Data.Interfaces
{
	public interface IReservationService
	{
		Task AddReservationAsync(AddReservationInfoViewModel model);
		Task<IEnumerable<AllReservationsViewModel>> AllReservationsAsync();
		Task<Reservation>UserReservationAsync(Guid userId);
		Task DeleteReservationAsync(int id);

        string? GetUserId();
	}
}

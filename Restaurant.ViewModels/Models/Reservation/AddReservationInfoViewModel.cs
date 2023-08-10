using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Models.Reservation
{
	using static Restaurant.Common.EntityValidationConstants.Reservation;
	using static Restaurant.Common.EntityValidationConstants.Table;
	public class AddReservationInfoViewModel
	{
		[Required]
		[MinLength(2)]
		[MaxLength(20)]
		public string FirstName { get; set; } = null!;        // TO DO CONSTANTS FOR FIRSTNAME, LASTNAME AND SO ON....!

		[Required]
		[MinLength(2)]
		[MaxLength(20)]
		public string LastName { get; set; } = null!;

		[Required]
		[Phone]
		public string Phone { get; set; } = null!;

		[Required]
		
		public TimeSpan Hour { get; set; }

		[Required]
		[Range(2,10)]
        public int Persons { get; set; }

		
        public DateTime Date { get; set; }
    }
}

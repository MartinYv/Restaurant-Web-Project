using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Models.Reservation
{
	using static Restaurant.Common.EntityValidationConstants.Table;
	public class AddReservationViewModel
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
		public int Phone { get; set; }

		[Required]
		[MinLength(9)]
		[MaxLength(ReservationHourMaxLength)]
		public int Hour { get; set; }
	}
}

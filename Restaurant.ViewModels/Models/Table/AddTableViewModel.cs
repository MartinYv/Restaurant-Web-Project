using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Models.Table
{
	using static Restaurant.Common.EntityValidationConstants.Table;
	public class AddTableViewModel
	{
		[Required]
		[Range(1, 20)]// to do constant
		public int Number { get; set; }

		[Required]
		[Range(SeatsMinLength, SeatsMaxLength)]
		public int Seats { get; set; }
	}
}

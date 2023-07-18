

namespace Restaurant.ViewModels.Models.Reservation
{
	public class AllReservationsViewModel
	{
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;      
		public string LastName { get; set; } = null!;
		public string Phone { get; set; } = null!;

        public string Hour { get; set; } = null!;
        public int Persons { get; set; }

		public int TableNumber { get; set; }
		public int TableSeats { get; set; }

	}
}

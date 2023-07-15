using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant.Common.EntityValidationConstants.Table;

namespace Restaurant.Data.Models
{
	public class Reservation
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(20)]
        public string FirstName { get; set; } = null!;        // TO DO CONSTANTS FOR FIRSTNAME, LASTNAME AND SO ON....!

		[Required]
		[MaxLength(20)]
		public string LastName { get; set; } = null!;

		[Required]
        public int Phone { get; set; }
        [Required]
		[MaxLength(ReservationHourMaxLength)]
		public int Hour { get; set; }

		[ForeignKey("Table")]
        public int TableId { get; set; }
		public Table Table { get; set; } = null!;


        [ForeignKey("Customer")]
		public Guid CustomerId { get; set; }
		public ApplicationUser Customer { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}

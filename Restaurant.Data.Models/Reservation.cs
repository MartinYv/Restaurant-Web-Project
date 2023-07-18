using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant.Common.EntityValidationConstants.Reservation;

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
		public string Phone { get; set; } = null!;


		[Required]

		public string Hour { get; set; } = null!;

		[Required]
        public DateTime Date { get; set; }

        [Required]
		[MaxLength(10)]
        public int Persons { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
		public Table Table { get; set; } = null!;


        [ForeignKey("Customer")]
		public Guid CustomerId { get; set; }
		public ApplicationUser Customer { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}

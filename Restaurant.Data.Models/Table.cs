using Microsoft.AspNetCore.Identity;
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
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(SeatsMaxLength)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(ReservationHourMaxLength)]
        public int Hour { get; set; }

        [Required]
        public bool IsReserved { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; } 
        public ApplicationUser? Customer { get; set; }
        public bool IsDeleted { get; set; }

    }
}

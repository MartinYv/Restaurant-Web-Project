using System.ComponentModel.DataAnnotations;

using static Restaurant.Common.EntityValidationConstants.Table;

namespace Restaurant.Data.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]// to do constant
		public int Number { get; set; }

        [MaxLength(SeatsMaxLength)]
        public int Seats { get; set; }


        public bool IsReserved { get; set; }       
        public bool IsDeleted { get; set; }

    }
}

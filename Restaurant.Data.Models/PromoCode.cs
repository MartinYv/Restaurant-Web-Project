namespace Restaurant.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	public class PromoCode
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[MaxLength(10)]
		public string Code { get; set; } = null!;

		[Range(0, int.MaxValue)]
		public int MaxUsageTimes { get; set; }

		public DateTime ExpirationDate { get; set; }

		[Range(0, int.MaxValue)]
		public int UsedTimes { get; set; }
		
		[Range(0, 100)]
		public int PromoPercent { get; set; }
		public bool IsDeleted { get; set; }
	}
}

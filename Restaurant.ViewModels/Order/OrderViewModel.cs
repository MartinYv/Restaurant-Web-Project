using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Order
{
    public class OrderViewModel
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string CreateDate { get; set; } = null!;
		public string IsCompleted { get; set; } = null!;
		public List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
	}
}

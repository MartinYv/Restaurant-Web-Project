using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Models.Menu
{
	public class AllMenusViewModel
	{
        public  int Id { get; set; }
        public string MenuType { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}

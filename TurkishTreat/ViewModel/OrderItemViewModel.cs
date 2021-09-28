using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TurkishTreat.ViewModel
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}

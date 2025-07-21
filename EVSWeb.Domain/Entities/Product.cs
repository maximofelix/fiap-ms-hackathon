using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Domain.Entities
{
    public class Product : BaseEntity
    {
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal SellPoints { get; set; } = 0M;
        public decimal Qtde { get; set; }
        public decimal Coast { get; set; }
        public decimal Price { get; set; }
        public bool IsAtive { get; set; } = true;
        //public Unit Unit { get; set; } = new Unit();
        public Category Category { get; set; } = new Category();

    }
}

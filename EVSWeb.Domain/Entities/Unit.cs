using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Domain.Entities
{
    public class Unit: BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        [InverseProperty("Unit")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

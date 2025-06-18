using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Domain.Entities
{
    public class Category : BaseEntity
    {
        //[StringLength(20)]
        public string Name { get; set; } = string.Empty;
        //[StringLength(255)]
        public string? Description { get; set; } 

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

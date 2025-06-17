using EVSWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Weight { get; set; }
        public decimal Qtde { get; set; }
        public decimal Coast { get; set; }
        public decimal Price { get; set; }
        public decimal SellPoints { get; set; } = 0M;
        public bool IsAtive { get; set; } = true;
        public Guid UnitId { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class CreatedProductDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Weight { get; set; }
        public decimal Qtde { get; set; }
        public decimal Coast { get; set; }
        public decimal Price { get; set; }
        public decimal SellPoints { get; set; } = 0M;
        public bool IsAtive { get; set; } = true;
        public Guid UnitId { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class UpdateProductDto
    {
        //public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Weight { get; set; }
        //public decimal Qtde { get; set; }
        //public decimal Coast { get; set; }
        //public decimal Price { get; set; }
        //public bool IsAtive { get; set; } = true;
        public Guid UnitId { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class ProductUpdateNameDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    public class DeleteProductDto
    {
        public Guid Id { get; set; }
    }
}

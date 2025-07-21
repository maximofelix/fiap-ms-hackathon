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
        public string? UrlImage { get; set; }
        public decimal Weight { get; set; }
        public decimal Qtde { get; set; }
        public decimal Coast { get; set; }
        public decimal Price { get; set; }
        public decimal SellPoints { get; set; } = 0M;
        public bool IsActive { get; set; } = true;
        public Guid CategoryId { get; set; }
    }

    public class CreatedProductDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
        public decimal Weight { get; set; }
        public decimal Qtde { get; set; }
        public decimal Coast { get; set; }
        public decimal Price { get; set; }
        public decimal SellPoints { get; set; } = 0M;
        public Guid CategoryId { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class UpdateProductDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
        public decimal Weight { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? UpdatedBy { get; set; }
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

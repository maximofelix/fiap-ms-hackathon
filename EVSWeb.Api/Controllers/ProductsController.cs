using EVSWeb.Application.DTOs;
using EVSWeb.Application.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace EVSWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService productService)
    {
        this._service = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var products = await _service.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductsAsync(CreatedProductDto product)
    {
        try
        {
            await _service.AddProductAsync(product);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao adicionar produto",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }
}

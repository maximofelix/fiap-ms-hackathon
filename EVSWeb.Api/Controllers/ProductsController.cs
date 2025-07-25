using EVSWeb.Application.DTOs;
using EVSWeb.Application.Interfaces.Products;
using EVSWeb.Domain.Messages;
using Microsoft.AspNetCore.Mvc;

namespace EVSWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
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
        try
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsByCategoryIdAsync(Guid categoryId)
    {
        try
        {
            var products = await _service.GetProductsByCategoryIdAsync(categoryId);
            if (products.Any())
                return Ok(products);
            else
                return NotFound(ProductMessages.PRODUCT_BYCATEGORYNOTFOUND);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProductByIdAsync(Guid id)
    {
        try
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product != null)
                return Ok(product);
            else
                return NotFound("Produto n�o encontrado");
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao Pesquisar o Produto",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByNameAsync(string name)
    {
        try
        {
            var products = await _service.GetProductByNameAsync(name);
            if (products != null)
                return Ok(products);
            else
                return NotFound("Nenhum produto encontrado com o nome indicado");
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao Pesquisar o Produto",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategoryAsync(Guid categoryId)
    {
        try
        {
            var products = await _service.GetProductsByCategoryIdAsync(categoryId);
            if (products != null)
                return Ok(products);
            else
                return NotFound("Nenhumm produto encontrado com a categoria indicada");
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao Pesquisar o Produto",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync(CreatedProductDto product)
    {
        try
        {
            var id = await _service.AddProductAsync(product);
            return Ok(new
            {
                Message = "Item cadastrado com sucesso", 
                Id = id
            });
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

    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync(Guid id, UpdateProductDto product)
    {
        try
        {
            await _service.UpdateProductAsync(id, product);
            return Ok(new
            {
                Message = "Item atualizado com sucesso"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao atualizar o produto",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }


    ////[HttpPut]
    ////public async Task<IActionResult> UpdateProductNameAsync(Guid id, string name)
    ////{
    ////    try
    ////    {
    ////        await _service.UpdateNameProductAsync(id, name);
    ////        return Ok();
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        return BadRequest(new
    ////        {
    ////            Mensagem = "Erro ao atualizar o nome do produto",
    ////            Erro = ex.Message,
    ////            Detalhe = ex.InnerException?.Message
    ////        });
    ////    }
    ////}

    [HttpDelete]
    public async Task<IActionResult> DeleteProductAsync(Guid productId)
    {
        try
        {
            await _service.DeleteProductAsync(productId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao excluir o produto",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }

    //[HttpPost]
    //public async Task<IActionResult> UpdateProductNameAsync(CreatedProductDto product)
    //{
    //    try
    //    {
    //        await _service.AddProductAsync(product);
    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new
    //        {
    //            Mensagem = "Erro ao adicionar produto",
    //            Erro = ex.Message,
    //            Detalhe = ex.InnerException?.Message
    //        });
    //    }
    //}

}
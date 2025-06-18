using EVSWeb.Application.DTOs;
using EVSWeb.Application.Interfaces.Categories;
using EVSWeb.Domain.Messages;
using Microsoft.AspNetCore.Mvc;

namespace EVSWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoriesController(ICategoryService Categorieservice)
    {
        this._service = Categorieservice;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        var Categories = await _service.GetAllCategoriesAsync();
        return Ok(Categories);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
    {
        try
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category != null)
                return Ok(category);
            else
                return NotFound(CategoryMessages.CATEGORY_NOTFOUND);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao Pesquisar a Categoria",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCategoryAsync(CreatedCategoryDto category)
    {
        try
        {
            await _service.AddCategoryAsync(category);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao adicionar a Categoria",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategoryAsync(Guid id, UpdateCategoryDto category)
    {
        try
        {
            await _service.UpdateCategoryAsync(id, category);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao atualizar a Categoria",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryAsync(Guid categoryId)
    {

        //var todos = await _service.GetAllCategoriesAsync();
        //foreach (var item in todos)
        //    try
        //    {
        //        await _service.DeleteCategoryAsync(item.Id);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        
        try
        {
            await _service.DeleteCategoryAsync(categoryId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Mensagem = "Erro ao excluir a Categoria",
                Erro = ex.Message,
                Detalhe = ex.InnerException?.Message
            });
        }
    }
}
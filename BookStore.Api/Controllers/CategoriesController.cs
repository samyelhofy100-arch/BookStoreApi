using BookStore.Api.DTOs;
using BookStore.Api.Models;
using BookStore.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IRepository<Category> categories, IUnitOfWork uow) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var all = await categories.GetAllAsync();
        return Ok(all.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CategoryDto>> Create(SaveCategoryDto dto)
    {
        var category = new Category { Name = dto.Name };
        await categories.AddAsync(category);
        await uow.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new CategoryDto { Id = category.Id, Name = category.Name });
    }
}
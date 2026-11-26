using BookStore.Api.DTOs;
using BookStore.Api.Models;
using BookStore.Api.Repositories;
using BookStore.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(
    IBookRepository books,
    IRepository<Category> categories,
    IRepository<Publisher> publishers,
    IUnitOfWork uow) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<BookDto>>> GetAll([FromQuery] BookFilter filter)
    {
        var page = await books.GetPagedAsync(filter);

        return Ok(new PagedResult<BookDto>
        {
            Items = page.Items.Select(b => b.ToDto()).ToList(),
            TotalCount = page.TotalCount,
            Page = page.Page,
            PageSize = page.PageSize
        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await books.GetByIdWithDetailsAsync(id);
        return book is null ? NotFound() : Ok(book.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDto>> Create(SaveBookDto dto)
    {
        var error = await ValidateReferencesAsync(dto);
        if (error is not null) return BadRequest(new { message = error });

        var book = new Book();
        book.Apply(dto);

        await books.AddAsync(book);
        await uow.SaveChangesAsync();

        var created = await books.GetByIdWithDetailsAsync(book.Id);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, created!.ToDto());
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, SaveBookDto dto)
    {
        var book = await books.GetByIdAsync(id);
        if (book is null) return NotFound();

        var error = await ValidateReferencesAsync(dto);
        if (error is not null) return BadRequest(new { message = error });

        book.Apply(dto);
        await uow.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await books.GetByIdAsync(id);
        if (book is null) return NotFound();

        books.Delete(book);
        await uow.SaveChangesAsync();

        return NoContent();
    }

    private async Task<string?> ValidateReferencesAsync(SaveBookDto dto)
    {
        if (await categories.GetByIdAsync(dto.CategoryId) is null)
            return $"Category {dto.CategoryId} does not exist.";

        if (dto.PublisherId is not null && await publishers.GetByIdAsync(dto.PublisherId.Value) is null)
            return $"Publisher {dto.PublisherId} does not exist.";

        return null;
    }
}
using BookStore.Api.Data;
using BookStore.Api.DTOs;
using BookStore.Api.Models;
using BookStore.Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Repositories;

public class BookRepository(AppDbContext db) : Repository<Book>(db), IBookRepository
{
    public Task<Book?> GetByIdWithDetailsAsync(int id)
        => Db.Books
            .Include(b => b.Category)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<PagedResult<Book>> GetPagedAsync(BookFilter filter)
    {
        var page = Math.Max(filter.Page, 1);
        var size = Math.Clamp(filter.PageSize, 1, 50);

        var query = Db.Books.AsNoTracking()
            .Include(b => b.Category)
            .Include(b => b.Publisher)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
            query = query.Where(b => b.Title.Contains(filter.Search) || b.Author.Contains(filter.Search));

        if (filter.CategoryId is not null)
            query = query.Where(b => b.CategoryId == filter.CategoryId);

        if (filter.PublisherId is not null)
            query = query.Where(b => b.PublisherId == filter.PublisherId);

        if (filter.Year is not null)
            query = query.Where(b => b.PublishYear == filter.Year);

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(b => b.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return new PagedResult<Book>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = size
        };
    }
}
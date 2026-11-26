using BookStore.Api.DTOs;
using BookStore.Api.Models;
using BookStore.Api.Utilities;

namespace BookStore.Api.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetByIdWithDetailsAsync(int id);
    Task<PagedResult<Book>> GetPagedAsync(BookFilter filter);
}
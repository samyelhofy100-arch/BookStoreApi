using BookStore.Api.Data;

namespace BookStore.Api.Repositories;

public class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => db.SaveChangesAsync();
}
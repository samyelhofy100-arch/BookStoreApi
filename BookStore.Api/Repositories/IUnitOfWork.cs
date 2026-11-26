namespace BookStore.Api.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
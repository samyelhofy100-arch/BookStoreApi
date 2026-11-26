using BookStore.Api.Data;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Repositories;

public class Repository<T>(AppDbContext db) : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db = db;

    public async Task<T?> GetByIdAsync(int id) => await Db.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> GetAllAsync()
        => await Db.Set<T>().AsNoTracking().ToListAsync();

    public async Task AddAsync(T entity) => await Db.Set<T>().AddAsync(entity);

    public void Update(T entity) => Db.Set<T>().Update(entity);

    public void Delete(T entity) => Db.Set<T>().Remove(entity);
}
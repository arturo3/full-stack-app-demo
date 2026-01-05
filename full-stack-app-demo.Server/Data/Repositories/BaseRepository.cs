using full_stack_app_demo.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace full_stack_app_demo.Server.Data.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected BaseRepository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    protected AppDbContext Context { get; }

    protected DbSet<T> DbSet { get; }

    public Task<List<T>> GetAllAsync()
    {
        return DbSet.AsNoTracking().ToListAsync();
    }

    public Task<T?> GetByIdAsync(int id)
    {
        return DbSet.FindAsync(id).AsTask();
    }

    public Task AddAsync(T entity)
    {
        return DbSet.AddAsync(entity).AsTask();
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public Task SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }
}

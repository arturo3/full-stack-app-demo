namespace full_stack_app_demo.Server.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    void Update(T entity);

    void Remove(T entity);

    Task SaveChangesAsync();
}

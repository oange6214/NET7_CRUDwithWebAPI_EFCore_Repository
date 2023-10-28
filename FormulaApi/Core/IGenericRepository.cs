namespace FormulaApi.Core;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(int id);
    T? GetById(int id);
    Task<bool> AddAsync(T entity);
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
}

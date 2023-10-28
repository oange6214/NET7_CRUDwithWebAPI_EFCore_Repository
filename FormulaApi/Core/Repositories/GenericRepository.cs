using FormulaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaApi.Core.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ILogger _logger;
    protected ApiDbContext _context;
    internal DbSet<T> _dbSet;

    public GenericRepository(
        ILogger logger, 
        ApiDbContext context)
    {
        _logger = logger;
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all entities.");
            throw;
        }
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            return await _dbSet.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting an entity by id: {id}");
            throw;
        }
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding an entity.");
            throw;
        }
    }

    public bool Add(T entity)
    {
        _dbSet.Add(entity);
        return true;
    }

    public bool Update(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating an entity.");
            throw;
        }
    }

    public bool Delete(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting an entity.");
            throw;
        }
    }
}

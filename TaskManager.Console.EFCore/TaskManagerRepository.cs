using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Console.EFCore
{
  public class TaskManagerRepository<T> : ITaskManagerRepository<T> where T : class
  {
    private readonly TaskManagerDbContext _context;
    private readonly DbSet<T> _dbSet;

    public TaskManagerRepository(TaskManagerDbContext context)
    {
      _context = context;
      _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
      return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
      return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
      await _dbSet.AddAsync(entity);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
      _dbSet.Update(entity);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
      var entity = await _dbSet.FindAsync(id);

      if (entity is not null)
      {
        _dbSet.Remove(entity);
      }
      
      await _context.SaveChangesAsync();
    }
  }
}

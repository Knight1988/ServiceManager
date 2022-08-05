using Microsoft.EntityFrameworkCore;
using ServiceManagerBackEnd.Interfaces.Models;
using ServiceManagerBackEnd.Interfaces.Repositories;

namespace ServiceManagerBackEnd.Repositories;

public abstract class BaseRepo<T> : IBaseRepo<T> where T : class, IBaseModel
{
    protected readonly ServiceManagerContext Context;
    private readonly DbSet<T> _dbSet;

    protected BaseRepo(ServiceManagerContext context, DbSet<T> dbSet)
    {
        Context = context;
        _dbSet = dbSet;
    }

    public virtual async Task<T?> GetAsync(int id)
    {
        return await _dbSet.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
    }

    public virtual async Task AddAsync(T model)
    {
        await _dbSet.AddAsync(model);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T model)
    {
        _dbSet.Update(model);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T model)
    {
        model.IsDeleted = true;
        _dbSet.Update(model);
        await Context.SaveChangesAsync();
    }
}
using ServiceManagerBackEnd.Interfaces.Models;
using ServiceManagerBackEnd.Interfaces.Repositories;
using ServiceManagerBackEnd.Interfaces.Services;

namespace ServiceManagerBackEnd.Services;

public abstract class BaseService<T> : IBaseService<T> where T : class, IBaseModel
{
    private readonly IBaseRepo<T> _baseRepo;

    protected BaseService(IBaseRepo<T> baseRepo)
    {
        _baseRepo = baseRepo;
    }

    public async Task<T?> GetAsync(int id)
    {
        return await _baseRepo.GetAsync(id);
    }

    public async Task AddAsync(T model)
    {
        await _baseRepo.AddAsync(model);
    }

    public async Task UpdateAsync(T model)
    {
        await _baseRepo.UpdateAsync(model);
    }

    public async Task DeleteAsync(T model)
    {
        await _baseRepo.DeleteAsync(model);
    }
}
using ServiceManagerBackEnd.Interfaces.Models;

namespace ServiceManagerBackEnd.Interfaces.Repositories;

public interface IBaseRepo<T> where T : class, IBaseModel
{
    Task<T?> GetAsync(int id);
    Task AddAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
}
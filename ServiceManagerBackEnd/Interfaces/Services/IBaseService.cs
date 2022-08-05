using ServiceManagerBackEnd.Interfaces.Models;

namespace ServiceManagerBackEnd.Interfaces.Services;

public interface IBaseService<T> where T: class, IBaseModel
{
    Task<T?> GetAsync(int id);
    Task AddAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
}
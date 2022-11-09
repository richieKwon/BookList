using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookModels
{
    public interface ICrudRepositoryBase<T, Iidentifier>
    {
        Task<T> AddAsync(T model);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Iidentifier id);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(Iidentifier id);
    }

    public interface IBookCrudRepository<T> : ICrudRepositoryBase<T, int>
    {
        //empty
    }
} 
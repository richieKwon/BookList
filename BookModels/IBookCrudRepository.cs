using System.Collections.Generic;
using System.Threading.Tasks;
using Dul.Articles;

namespace BookModels
{
    // public interface ICrudRepositoryBase<T, Tidentifier>
    // {
    //     Task<T> AddAsync(T model);
    //     Task<List<T>> GetAllAsync();
    //     Task<T> GetByIdAsync(Tidentifier id);
    //     Task<bool> UpdateAsync(T model);
    //     Task<bool> DeleteAsync(Tidentifier id);
    // }

    public interface IBookCrudRepository<T> : ICrudRepositoryBase<T, int> where T : class
    {
        //empty
    }
} 
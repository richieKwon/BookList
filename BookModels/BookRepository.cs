using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookModels
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _bookDbContext;
        private readonly ILogger _logger;

        public BookRepository(BookDbContext bookDbContext, ILoggerFactory loggerFactory)
        {
            _bookDbContext = bookDbContext;
            _logger = loggerFactory.CreateLogger(nameof(BookRepository));
        }

        #region AddAsync

        public async Task<Book> AddAsync(Book model)
        {
            try
            {
                _bookDbContext.Books.Add(model);
                await _bookDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger?.LogError($"Error({nameof(AddAsync)}):{e.Message}");
            }
            return model;
        }
        

        #endregion

        #region GetAllAsync

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookDbContext.Books.OrderByDescending(book => book.Id)
                .ToListAsync();
        }  

        #endregion

        #region GetByIdAsync

        public async Task<Book> GetByIdAsync(int id)
        {
            var model = await _bookDbContext.Books
                .SingleOrDefaultAsync(book => book.Id == id);
            return model;
        }

        #endregion

        #region UpdateAsync

        public async Task<bool> UpdateAsync(Book model)
        {
            try
            {
                _bookDbContext.Update(model);
                return (await _bookDbContext.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Error({nameof(UpdateAsync)}):{e.Message}");
            }

            return false;
        }

        #endregion

        #region DeleteAsync

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var model = await _bookDbContext.Books.FindAsync(id);
                _bookDbContext.Remove(model);
                return (await _bookDbContext.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger?.LogError($"Error({nameof(DeleteAsync)}):{e.Message}");
            }

            return false;
        }

        #endregion
    }
}
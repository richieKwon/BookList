using System;
using System.Linq;
using System.Threading.Tasks;
using BookModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookListTests
{
    // InMemory database (Nuget Package - EntityFramework Inmemory) 
    [TestClass]
    public class BookRepositoryTest
    {
        [TestMethod]
        public async Task BookRepositoryMethodTest(){
            {
                // Creating DbContextOption<T> Object and ILoggerFactory Object
                var options = new DbContextOptionsBuilder<BookDbContext>()
                    .UseInMemoryDatabase(databaseName: $"BookApp{Guid.NewGuid()}").Options;
                var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
                var factory = serviceProvider.GetService<ILoggerFactory>();


                #region AddingAsync test

                using (var context = new BookDbContext(options))
                {
                    // Checking if the Database is created
                    context.Database.EnsureCreated();

                    var repository = new BookRepository(context, factory);
                    
                    // the first data
                    var model = new Book { Title = "Richie's Engineering Course", Description = "Data Engineering for beginners"};
                    await repository.AddAsync(model);
                }

                // the total records   
                using (var context = new BookDbContext(options))
                {
                    Assert.AreEqual(1, await context.Books.CountAsync());

                    // checking if the first record is correct
                    var model = await context.Books.Where(book => book.Id == 1).SingleOrDefaultAsync();
                    Assert.AreEqual("Richie's Engineering Course", model.Title);
                }

                #endregion


                #region GetAllAsync Test

                using (var context = new BookDbContext(options))
                {
                    var repository = new BookRepository(context, factory);
                    var model = new Book { Title = "BigData", Description = "BigData with Spark"};
                    var model2 = new Book { Title = "Dashboard", Description = "PowerBI"};

                    await repository.AddAsync(model);
                    await repository.AddAsync(model2);
                }

                using (var context = new BookDbContext(options))
                {
                    var repository = new BookRepository(context, factory);
                    var models = await repository.GetAllAsync();
                    Assert.AreEqual(3, models.Count());
                }
             
                #endregion
                
                
            }
        }
    }
}       
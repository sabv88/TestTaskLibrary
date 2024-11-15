using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private ApplicationDbContext _applicationDbContext;
        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        ///Возвращает книгу с наибольшей стоимостью опубликованного тиража
        /// </summary>
        public Task<Book> GetBook()
        {
            return _applicationDbContext.Books.OrderByDescending(b => b.QuantityPublished * b.Price).FirstOrDefaultAsync();
        }

        /// <summary>
        ///Возвращает книги, в названии которой содержится "Red" и которые опубликованы после выхода альбома "Carolus Rex" группы Sabaton
        /// </summary>
        public Task<List<Book>> GetBooks()
        {
            DateTime albumReleaseDate = new DateTime(2012, 5, 22);
            return _applicationDbContext.Books.Where(b => b.Title.Contains("Red") && b.PublishDate > albumReleaseDate).ToListAsync();
        }
    }
}

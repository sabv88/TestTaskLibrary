using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private ApplicationDbContext _applicationDbContext;
        public AuthorService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        ///Возвращает автора, который написал книгу с самым длинным названием ( в случае если таких авторов окажется несколько, необходимо вернуть автора с наименьшим Id)
        /// </summary>
        public Task<Author> GetAuthor()
        {
            var authorWithLongestBookTitle = _applicationDbContext.Books.OrderByDescending(b => b.Title.Length).ThenBy(b => b.AuthorId).Select(b => new { b.AuthorId, b.Title }).FirstOrDefault();

            if (authorWithLongestBookTitle != null)
            {
                return _applicationDbContext.Authors.FirstOrDefaultAsync(x => x.Id == authorWithLongestBookTitle.AuthorId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///Возвращает авторов, написавших четное количество книг, изданных после 2015 года
        /// </summary>
        public Task<List<Author>> GetAuthors()
        {
            return _applicationDbContext.Authors.Where(a => a.Books.Where(b => b.PublishDate.Year > 2015).Count() % 2 == 0 && a.Books.Where(b => b.PublishDate.Year > 2015).Count() > 0).ToListAsync();
        }
    }
}

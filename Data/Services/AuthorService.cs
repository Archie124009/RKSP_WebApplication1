using WebApplication1.Data.Models;

namespace WebApplication1.Data.Services
{
    public class AuthorService
    {
        public async Task<Author> AddAuthor(Author author)
        {
            DataSource.GetInstance()._authors.Add(author);
            return await Task.FromResult(author);
        }

    }
}

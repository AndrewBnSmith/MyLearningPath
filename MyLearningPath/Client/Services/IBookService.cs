using MyLearningPath.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLearningPath.Client.Services
{
    public interface IBookService
    {
        event Action OnChange;
        List<BookType> BookTypes { get; set; }
        List<Book> Books { get; set; }
        Task<List<Book>> GetBooks();
        Task GetBookType();
        Task<Book> GetBook(int id);
        Task<List<Book>> CreateSingleBook(Book book);
        Task<List<Book>> UpdateBook(Book book, int id);
        Task<List<Book>> DeleteBook(int id);
    }
}

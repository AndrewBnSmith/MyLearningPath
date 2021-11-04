using MyLearningPath.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyLearningPath.Client.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<BookType> BookTypes { get; set; } = new List<BookType>();
        public List<Book> Books { get; set; } = new List<Book>();
   
        public event Action OnChange;

        public async Task<List<Book>> CreateSingleBook(Book book)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/book", book);
            Books = await result.Content.ReadFromJsonAsync<List<Book>>();
            OnChange.Invoke();
            return Books;
        }

        public async Task<List<Book>> DeleteBook(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/book/{id}");
            Books = await result.Content.ReadFromJsonAsync<List<Book>>();
            OnChange.Invoke();
            return Books;
        }

        public async Task GetBookTypes()
        {
           BookTypes = await _httpClient.GetFromJsonAsync<List<BookType>>($"api/book/booktypes");
        }

        public async Task<Book> GetBook(int id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"api/book/{id}");
        }

        public async Task<List<Book>> GetBooks()
        {
            Books = await _httpClient.GetFromJsonAsync<List<Book>>("api/book");
            return Books;
        }

        public async Task<List<Book>> UpdateBook(Book book, int id)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/book/{id}", book);
           Books = await result.Content.ReadFromJsonAsync<List<Book>>();
            OnChange.Invoke();
            return Books;
        }

       
    }
}

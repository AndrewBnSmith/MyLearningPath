using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLearningPath.Server.Data;
using MyLearningPath.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLearningPath.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        static List<BookType> bookTypes = new List<BookType>
        {
            new BookType { Name = "CSharp" },
            new BookType { Name = "Java" },
        };

        List<Book> books = new List<Book>
        {
         new Book {Id = 1, Title = "CSharp Beginner", Author = "Oreilly", BookType = bookTypes[0] },
         new Book {Id = 2, Title = "Java Beginner", Author = "Camden", BookType = bookTypes[1] },
        };

        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return base.Ok(await GetDbBooks());
        }

        private async Task<List<Book>> GetDbBooks()
        {
            return await _context.Books.Include(sh => sh.BookType).ToListAsync();
        }

        [HttpGet("bookType")]
        public async Task<IActionResult> GetBookType()
        {
            return Ok(await _context.BookTypes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleBook(int id)
        {
            var book = await _context.Books
                .Include(sh => sh.BookType)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (book == null)
                return NotFound("Book wasn't found.");

            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSingleBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok(await GetDbBooks());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Book book, int id)
        {
            var dbBook = await _context.Books
                .Include(sh => sh.BookType)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (dbBook == null)
                return NotFound("Book wasn't found.");

            dbBook.Title = book.Title;
            dbBook.Author = book.Author;

            dbBook.BookTypeId = book.BookTypeId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbBooks());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var dbBook = await _context.Books
                .Include(sh => sh.BookType)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (dbBook == null)
                return NotFound("Book wasn't found.");

            _context.Books.Remove(dbBook);
            await _context.SaveChangesAsync();

            return Ok(await GetDbBooks());
        }


    }
}

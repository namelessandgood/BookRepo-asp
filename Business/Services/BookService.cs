using Microsoft.EntityFrameworkCore;
using BooksArchivingSystem.Data;
using BooksArchivingSystem.Data.Models;
using BooksArchivingSystem.Business.DTOs;
using BooksArchivingSystem.Business.Services.Interfaces;

namespace BooksArchivingSystem.Business.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .ToListAsync();

            return books.Select(MapToDto);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book == null ? null : MapToDto(book);
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                ISBN = bookDto.ISBN,
                Publisher = bookDto.Publisher,
                PublicationDate = bookDto.PublicationDate,
                Description = bookDto.Description,
                CategoryId = bookDto.CategoryId,
                Pages = bookDto.Pages,
                Language = bookDto.Language,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Add author relationships
            if (bookDto.AuthorIds.Any())
            {
                var bookAuthors = bookDto.AuthorIds.Select(authorId => new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = authorId,
                    CreatedAt = DateTime.UtcNow
                });

                _context.BookAuthors.AddRange(bookAuthors);
                await _context.SaveChangesAsync();
            }

            return await GetBookByIdAsync(book.Id) ?? new BookDto();
        }

        public async Task<BookDto> UpdateBookAsync(BookDto bookDto)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.Id == bookDto.Id);

            if (book == null)
                throw new ArgumentException("Book not found");

            // Update book properties
            book.Title = bookDto.Title;
            book.ISBN = bookDto.ISBN;
            book.Publisher = bookDto.Publisher;
            book.PublicationDate = bookDto.PublicationDate;
            book.Description = bookDto.Description;
            book.CategoryId = bookDto.CategoryId;
            book.Pages = bookDto.Pages;
            book.Language = bookDto.Language;
            book.UpdatedAt = DateTime.UtcNow;

            // Update author relationships
            _context.BookAuthors.RemoveRange(book.BookAuthors);

            if (bookDto.AuthorIds.Any())
            {
                var bookAuthors = bookDto.AuthorIds.Select(authorId => new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = authorId,
                    CreatedAt = DateTime.UtcNow
                });

                _context.BookAuthors.AddRange(bookAuthors);
            }

            await _context.SaveChangesAsync();
            return await GetBookByIdAsync(book.Id) ?? new BookDto();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return false;

            _context.BookAuthors.RemoveRange(book.BookAuthors);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm)
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Where(b => b.Title.Contains(searchTerm) ||
                           (b.ISBN != null && b.ISBN.Contains(searchTerm)) ||
                           (b.Publisher != null && b.Publisher.Contains(searchTerm)) ||
                           b.BookAuthors.Any(ba => ba.Author.Name.Contains(searchTerm)))
                .ToListAsync();

            return books.Select(MapToDto);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId)
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();

            return books.Select(MapToDto);
        }

        private static BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                PublicationDate = book.PublicationDate,
                Description = book.Description,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.Name,
                Pages = book.Pages,
                Language = book.Language,

                Authors = book.BookAuthors.Select(ba => new AuthorDto
                {
                    Id = ba.Author.Id,
                    Name = ba.Author.Name,
                    Biography = ba.Author.Biography,
                    BirthDate = ba.Author.BirthDate,
                    DeathDate = ba.Author.DeathDate,
                    Nationality = ba.Author.Nationality,
                    CreatedAt = ba.Author.CreatedAt
                }).ToList(),

                AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList(),
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };
        }
    }
}

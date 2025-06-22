using BooksArchivingSystem.Business.DTOs;

namespace BooksArchivingSystem.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(BookDto bookDto);
        Task<BookDto> UpdateBookAsync(BookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);
        Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId);
    }
}

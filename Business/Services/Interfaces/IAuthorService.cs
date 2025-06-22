using BooksArchivingSystem.Business.DTOs;

namespace BooksArchivingSystem.Business.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
        Task<AuthorDto> UpdateAuthorAsync(AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}

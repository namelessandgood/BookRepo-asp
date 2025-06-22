using Microsoft.EntityFrameworkCore;
using BooksArchivingSystem.Data;
using BooksArchivingSystem.Data.Models;
using BooksArchivingSystem.Business.DTOs;
using BooksArchivingSystem.Business.Services.Interfaces;

namespace BooksArchivingSystem.Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors.Select(MapToDto);
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            return author == null ? null : MapToDto(author);
        }

        public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Biography = authorDto.Biography,
                BirthDate = authorDto.BirthDate,
                DeathDate = authorDto.DeathDate,
                Nationality = authorDto.Nationality,
                CreatedAt = DateTime.UtcNow
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return MapToDto(author);
        }

        public async Task<AuthorDto> UpdateAuthorAsync(AuthorDto authorDto)
        {
            var author = await _context.Authors.FindAsync(authorDto.Id);
            if (author == null)
                throw new ArgumentException("Author not found");

            author.Name = authorDto.Name;
            author.Biography = authorDto.Biography;
            author.BirthDate = authorDto.BirthDate;
            author.DeathDate = authorDto.DeathDate;
            author.Nationality = authorDto.Nationality;

            await _context.SaveChangesAsync();
            return MapToDto(author);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        private static AuthorDto MapToDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                Nationality = author.Nationality,
                CreatedAt = author.CreatedAt
            };
        }
    }
}

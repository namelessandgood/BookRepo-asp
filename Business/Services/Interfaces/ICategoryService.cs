using BooksArchivingSystem.Business.DTOs;

namespace BooksArchivingSystem.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}

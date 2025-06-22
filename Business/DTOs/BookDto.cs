using System.ComponentModel.DataAnnotations;

namespace BooksArchivingSystem.Business.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "ISBN cannot exceed 20 characters")]
        public string? ISBN { get; set; }

        [StringLength(100, ErrorMessage = "Publisher cannot exceed 100 characters")]
        public string? Publisher { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be greater than 0")]
        public int Pages { get; set; }

        public string? Language { get; set; }

        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
        public List<int> AuthorIds { get; set; } = new List<int>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

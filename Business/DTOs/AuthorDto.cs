using System.ComponentModel.DataAnnotations;

namespace BooksArchivingSystem.Business.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Biography cannot exceed 500 characters")]
        public string? Biography { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeathDate { get; set; }

        public string? Nationality { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

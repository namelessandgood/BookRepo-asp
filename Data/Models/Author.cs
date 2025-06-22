using System.ComponentModel.DataAnnotations;

namespace BooksArchivingSystem.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }

        public string? Nationality { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}

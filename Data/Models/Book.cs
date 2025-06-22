using System.ComponentModel.DataAnnotations;

namespace BooksArchivingSystem.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(20)]
        public string? ISBN { get; set; }

        [StringLength(100)]
        public string? Publisher { get; set; }

        public DateTime? PublicationDate { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int Pages { get; set; }
        public string? Language { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace BooksArchivingSystem.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

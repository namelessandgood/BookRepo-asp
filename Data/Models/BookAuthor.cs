namespace BooksArchivingSystem.Data.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

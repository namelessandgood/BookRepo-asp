using BooksArchivingSystem.Business.DTOs;

namespace BooksArchivingSystem.Models
{
  public class DashboardViewModel
  {
    public int TotalBooks { get; set; }
    public int TotalAuthors { get; set; }
    public int TotalCategories { get; set; }
    public int BooksAddedThisMonth { get; set; }
    public List<BookDto> RecentBooks { get; set; } = new List<BookDto>();
  }
}

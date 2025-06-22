using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BooksArchivingSystem.Models;
using BooksArchivingSystem.Business.Services.Interfaces;

namespace BooksArchivingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;

    public HomeController(ILogger<HomeController> logger,
        IBookService bookService,
        IAuthorService authorService,
        ICategoryService categoryService
      )
    {
        _logger = logger;
        _bookService = bookService;
        _authorService = authorService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        // Get statistics for dashboard
        var books = await _bookService.GetAllBooksAsync();
        var authors = await _authorService.GetAllAuthorsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();

        // Create dashboard model
        var dashboardData = new DashboardViewModel
        {
            TotalBooks = books.Count(),
            TotalAuthors = authors.Count(),
            TotalCategories = categories.Count(),
            BooksAddedThisMonth = books.Count(b => b.CreatedAt.Month == DateTime.Now.Month && b.CreatedAt.Year == DateTime.Now.Year),
            RecentBooks = books.OrderByDescending(b => b.CreatedAt).Take(6).ToList()
        };

        return View(dashboardData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

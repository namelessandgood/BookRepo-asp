using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BooksArchivingSystem.Business.DTOs;
using BooksArchivingSystem.Business.Services.Interfaces;

namespace BooksArchivingSystem.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;

        public BooksController(IBookService bookService, IAuthorService authorService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchTerm = "", int categoryId = 0)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;

            IEnumerable<BookDto> books;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = await _bookService.SearchBooksAsync(searchTerm);
            }
            else if (categoryId > 0)
            {
                books = await _bookService.GetBooksByCategoryAsync(categoryId);
            }
            else
            {
                books = await _bookService.GetAllBooksAsync();
            }

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.CreateBookAsync(bookDto);
                    TempData["SuccessMessage"] = "Book created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating book: {ex.Message}");
                }
            }

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
            return View(bookDto);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(int id, BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBookAsync(bookDto);
                    TempData["SuccessMessage"] = "Book updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating book: {ex.Message}");
                }
            }

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Authors = await _authorService.GetAllAuthorsAsync();
            return View(bookDto);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _bookService.DeleteBookAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Book deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Book not found!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting book: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

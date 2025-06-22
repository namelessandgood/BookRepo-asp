using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BooksArchivingSystem.Business.DTOs;
using BooksArchivingSystem.Business.Services.Interfaces;

namespace BooksArchivingSystem.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        [Authorize(Roles = "Admin,Librarian")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Create(AuthorDto authorDto)
        {
            if (ModelState.IsValid)
            {
                await _authorService.CreateAuthorAsync(authorDto);
                TempData["SuccessMessage"] = "Author created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(authorDto);
        }

        // GET: Authors/Edit/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Edit(int id, AuthorDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authorService.UpdateAuthorAsync(authorDto);
                    TempData["SuccessMessage"] = "Author updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Failed to update author.");
                }
            }
            return View(authorDto);
        }

        // GET: Authors/Delete/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Author deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete author. Author may be associated with books.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

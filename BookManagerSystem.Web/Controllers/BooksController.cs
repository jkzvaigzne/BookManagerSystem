using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagerSystem.Web.Models.Books;
using BookManagerSystem.Web.Services;
using BookManagerSystem.Web.Helpers.Validations;

namespace BookManagerSystem.Web.Controllers
{
    public class BooksController(IBookService _bookService): Controller
    {
        // GET: Books
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            // Fetch all books using the book service
            var viewData = await _bookService.GetAllBooksAsync();
            // Pass the view model list to the view for rendering
            return View(viewData);
        }

        // GET: book/detail/5
        [HttpGet("book/detail/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.Get<BookReadOnlyVM>(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: book/create
        [HttpGet("book/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: book/create
        [HttpPost("book/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVM bookCreate)
        {
            if (await _bookService.CheckIfTitleExists(bookCreate.Title)) 
            {
                ModelState.AddModelError(nameof(bookCreate.Title), ValidationMessages.NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _bookService.Create(bookCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(bookCreate);
        }

        // GET: book/edit/5
        [HttpGet("book/edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.Get<BookEditVM>(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: book/edit/5
        [HttpPost("book/edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookEditVM bookEdit)
        {
            if (id != bookEdit.Id)
            {
                return NotFound();
            }

            if (await _bookService.CheckIfTitleExistsForEdit(bookEdit))
            {
                ModelState.AddModelError(nameof(bookEdit.Title), ValidationMessages.NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.Edit(bookEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_bookService.BookExists(bookEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookEdit);
        }

        // GET: book/delete/5
        [HttpGet("book/delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.Get<BookReadOnlyVM>(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: book/delete/5
        [HttpPost("book/delete/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

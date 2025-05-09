using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagerSystem.Web.Models.Books;
using BookManagerSystem.Web.Services;

namespace BookManagerSystem.Web.Controllers
{
    public class BooksController(IBookService _bookService): Controller
    {
        private const string NameExistsValidationMessage = "Book title already exists.";
        
        // GET: Books
        public async Task<IActionResult> Index()
        {
            // Fetch all books using the book service
            var viewData = await _bookService.GetAllBooksAsync();
            // Pass the view model list to the view for rendering
            return View(viewData);
        }

        // GET: Books/Details/5
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

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVM bookCreate)
        {
            if (await _bookService.CheckIfTitleExists(bookCreate.Title)) 
            {
                ModelState.AddModelError(nameof(bookCreate.Title), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _bookService.Create(bookCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(bookCreate);
        }

        // GET: Books/Edit/5
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

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookEditVM bookEdit)
        {
            if (id != bookEdit.Id)
            {
                return NotFound();
            }

            if (await _bookService.CheckIfTitleExistsForEdit(bookEdit))
            {
                ModelState.AddModelError(nameof(bookEdit.Title), NameExistsValidationMessage);
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

        // GET: Books/Delete/5
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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagerSystem.Web.Data;
using BookManagerSystem.Web.Models.Books;
using AutoMapper;

namespace BookManagerSystem.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const string NameExistsValidationMessage = "Book Title already exists.";

        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var data = await _context.Books.ToListAsync();
            // Convert data model into view model - AutoMapper
            var viewData = _mapper.Map<List<BookReadOnlyVM>>(data);
            // Return view model to the view
            return View(viewData);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var viewData = _mapper.Map<BookReadOnlyVM>(book);
            return View(viewData);
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
            if (await CheckIfTitleExists(bookCreate.Title)) 
            {
                ModelState.AddModelError(nameof(bookCreate.Title), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                var book = _mapper.Map<Book>(bookCreate);
                _context.Add(book);
                await _context.SaveChangesAsync();
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

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewData = _mapper.Map<BookEditVM>(book);
            return View(viewData);
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

            if (await CheckIfTitleExistsForEdit(bookEdit))
            {
                ModelState.AddModelError(nameof(bookEdit.Title), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var book = _mapper.Map<Book>(bookEdit);
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookEdit.Id))
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

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var viewData = _mapper.Map<BookReadOnlyVM>(book);
            return View(viewData);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
        // Check if Book Title Exists
        private async Task<bool> CheckIfTitleExists(string title)
        {
            var lowercaseTitle = title.ToLower();
            return await _context.Books.AnyAsync(e => e.Title.ToLower().Equals(lowercaseTitle));
        }
        private async Task<bool> CheckIfTitleExistsForEdit(BookEditVM bookEdit)
        {
            var lowercaseTitle = bookEdit.Title.ToLower();
            return await _context.Books.AnyAsync(e =>
                    e.Title.ToLower() == lowercaseTitle && e.Id != bookEdit.Id);
        }
    }
}

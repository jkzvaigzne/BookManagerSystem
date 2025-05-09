using AutoMapper;
using BookManagerSystem.Web.Data;
using BookManagerSystem.Web.Models.Books;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookManagerSystem.Web.Services;

public class BookService(ApplicationDbContext _context, IMapper _mapper) : IBookService
{
    public async Task<List<BookReadOnlyVM>> GetAllBooksAsync()
    {
        var data = await _context.Books.ToListAsync();
        var viewData = _mapper.Map<List<BookReadOnlyVM>>(data);
        return viewData;
     }

    public async Task<T?> Get<T>(int id) where T : class
    {
        // Retrieve the book entity from the database by ID
        var data = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
        {
            return null;
        }
        // Map the entity to the specified view model type - AutoMapper
        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Remove(int id)
    {
        var data = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (data != null)
        {
            // Remove the book entity from the database
            _context.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Edit(BookEditVM model)
    {
        var book = _mapper.Map<Book>(model);
        _context.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task Create(BookCreateVM model)
    {
        var book = _mapper.Map<Book>(model);
        _context.Add(book);
        await _context.SaveChangesAsync();
    }
    public bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
    // Check if Book Title Exists
    public async Task<bool> CheckIfTitleExists(string title)
    {
        var lowercaseTitle = title.ToLower();
        return await _context.Books.AnyAsync(e => e.Title.ToLower().Equals(lowercaseTitle));
    }
    public async Task<bool> CheckIfTitleExistsForEdit(BookEditVM bookEdit)
    {
        var lowercaseTitle = bookEdit.Title.ToLower();
        return await _context.Books.AnyAsync(e =>
                e.Title.ToLower() == lowercaseTitle && e.Id != bookEdit.Id);
    }
}


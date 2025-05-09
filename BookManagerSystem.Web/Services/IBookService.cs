using BookManagerSystem.Web.Models.Books;

namespace BookManagerSystem.Web.Services
{
    public interface IBookService
    {
        bool BookExists(int id);
        Task<bool> CheckIfTitleExists(string title);
        Task<bool> CheckIfTitleExistsForEdit(BookEditVM bookEdit);
        Task Create(BookCreateVM model);
        Task Edit(BookEditVM model);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<BookReadOnlyVM>> GetAllBooksAsync();
        Task Remove(int id);
    }
}
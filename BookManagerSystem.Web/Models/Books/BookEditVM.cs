using System.ComponentModel.DataAnnotations;

namespace BookManagerSystem.Web.Models.Books
{
    public class BookEditVM : BaseBookVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = "Title must be between 4 and 150 characters long.")]
        public string Title { get; set; }
        [Required]
        [Length(4, 150, ErrorMessage = "Author must be between 4 and 150 characters long.")]
        public string Author { get; set; }
        [Display(Name = "Book Published Date")]
        public DateTime? PublishedDate { get; set; }
    }
}

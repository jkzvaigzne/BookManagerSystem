using BookManagerSystem.Web.Helpers.Validations;
using System.ComponentModel.DataAnnotations;

namespace BookManagerSystem.Web.Models.Books
{
    public class BookEditVM : BaseBookVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = ValidationMessages.NameLengthValidationMessage)]
        public string Title { get; set; }
        [Required]
        [Length(4, 150, ErrorMessage = ValidationMessages.AuthorLengthValidationMessage)]
        public string Author { get; set; }
        [Display(Name = "Book Published Date")]
        public DateTime? PublishedDate { get; set; }
    }
}

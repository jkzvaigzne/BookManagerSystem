using System.ComponentModel.DataAnnotations;

namespace BookManagerSystem.Web.Models.Books
{
    public class BookReadOnlyVM : BaseBookVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Display(Name = "Book Published Date")]
        public DateTime? PublishedDate { get; set; }
    }
}

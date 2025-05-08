using System.ComponentModel.DataAnnotations;

namespace BookManagerSystem.Web.Models.Books
{
    public class BookCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}

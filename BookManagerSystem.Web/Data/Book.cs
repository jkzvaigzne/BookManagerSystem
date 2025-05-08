using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagerSystem.Web.Data
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Author { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}

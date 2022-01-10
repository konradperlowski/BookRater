using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookRater.Models
{
    public class BookWithCover
    {
        [Required] public string Title { get; init; }
        [Required] public string Author { get; init; }
        [Required]
        [Display(Name = "Book cover")]
        public IFormFile File { get; init; }
    }
}
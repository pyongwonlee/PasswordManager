using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Models.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie Title is required")]
        [MaxLength(250, ErrorMessage = "Movie Title cannot exceeds 250 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Released Year is required")]
        public int Year { get; set; }

        [Range(0,100)]
        public int? Tomatometer { get; set; }

        [Display(Name = "IMDB Rating")]
        [Range(0.0, 10.0)]
        public double? IMDBRating { get; set; }

        [Required(ErrorMessage = "Director is required")]
        [Display(Name = "Director")]
        public int DirectorId { get; set; }

        [ForeignKey("DirectorId")]
        public virtual Director Director { get; set; }
    }
}
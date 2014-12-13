using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models.Entities
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Director Name is required")]
        [MaxLength(100, ErrorMessage = "Director Name cannot exceeds 100 characters")]
        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
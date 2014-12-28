using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models.Entities
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Province Name is required")]
        [MaxLength(100, ErrorMessage = "Province Name cannot exceeds 100 characters")]
        [MinLength(2, ErrorMessage = "Province Name must have at least 2 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abbreviation Name is required")]
        [MaxLength(5, ErrorMessage = "Abbreviation Name cannot exceeds 5 characters")]
        [MinLength(2, ErrorMessage = "Abbreviation Name must have at least 2 characters")]
        public string Abbreviation { get; set; }

        public List<City> Cities { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage="Catgeory Name is required")]
        [MaxLength(250, ErrorMessage="Catgeory Name cannot exceeds 250 characters")]
        [MinLength(2, ErrorMessage="Catgeory Name must have at least 2 characters")]
        public string Name { get; set; }

        public List<Company> Companies { get; set; }
    }
}
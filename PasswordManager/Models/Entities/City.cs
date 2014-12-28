using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Models.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [MaxLength(100, ErrorMessage = "City Name cannot exceeds 100 characters")]
        [MinLength(2, ErrorMessage = "City Name must have at least 2 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }

        public List<Center> Centers { get; set; }
    }
}
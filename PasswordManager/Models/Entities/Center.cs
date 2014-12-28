using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PasswordManager.Models.Entities
{
    public class Center
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [MaxLength(100, ErrorMessage = "City Name cannot exceeds 100 characters")]
        [MinLength(2, ErrorMessage = "City Name must have at least 2 characters")]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(250, ErrorMessage = "Web Address cannot exceeds 250 characters")]
        [Display(Name = "Web Address")]
        public string WebAddress { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
    }
}
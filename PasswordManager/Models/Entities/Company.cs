using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Models.Entities
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage="Company Name is required")]
        [MaxLength(250, ErrorMessage="Comapny Name cannot exceeds 250 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company Description is required")]
        public string Description { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(250, ErrorMessage = "Web Address cannot exceeds 250 characters")]
        [Display(Name="Web Address")]
        public string WebAddress { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name="Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<Password> Passwords { get; set; }
        
    }
}
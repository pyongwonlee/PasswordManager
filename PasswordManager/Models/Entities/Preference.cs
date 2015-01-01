using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models.Entities
{
    public class Preference
    {
        [MaxLength(100, ErrorMessage = "Preference Key name cannot exceeds 100 characters")]
        [Required]
        [Key]
        public string Key { get; set; }

        [Required]
        public int Value { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Models.Entities
{
    public class Password
    {
        [Key]
        public int PasswordId { get; set; }

        [Required(ErrorMessage="User Name is required")]
        [MaxLength(250, ErrorMessage = "User Name cannot exceeds 250 characters")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Password cannot exceeds 250 characters")]
        [Display(Name = "Password")]
        public string PasswordCode { get; set; }

        [Display(Name = "Note 1")]
        public string Note1 { get; set; }
        [Display(Name = "Note 2")]
        public string Note2 { get; set; }
        [Display(Name = "Note 3")]
        public string Note3 { get; set; }
        [Display(Name = "Note 4")]
        public string Note4 { get; set; }
        [Display(Name = "Note 5")]
        public string Note5 { get; set; }

        [MaxLength(250, ErrorMessage = "Comment cannot exceeds 250 characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}
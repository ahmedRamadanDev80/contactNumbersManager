using System.ComponentModel.DataAnnotations;

namespace contactNumbersManager.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^01\d{9}$", ErrorMessage = "Phone number must start with '01' and be exactly 11 digits.")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters.")]
        public string Notes { get; set; }
    }
}

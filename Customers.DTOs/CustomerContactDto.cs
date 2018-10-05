using System.ComponentModel.DataAnnotations;

namespace Customers.DTOs
{
    /// <summary>
    ///  Used As Data Transfer object for the Customer Contact Table Entity Model
    /// </summary>
    public class CustomerContactDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}

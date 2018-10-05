using System.ComponentModel.DataAnnotations;

namespace Customers.DTOs
{
    /// <summary>
    ///  Used As Data Transfer object for the Customer Table Entity Model
    /// </summary>
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Range(0, 10000)]
        public int NumberOfEmployees { get; set; }

        public CustomerContactDto CustomerContact { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Customers.DAL.Models
{
    /// <summary>
    /// Customers Table Entity
    /// </summary>
    public class Customer : BaseKey
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Range(0, 10000)]
        public int NumberOfEmployees { get; set; }

        public CustomerContact CustomerContact { get; set; }
    }
}

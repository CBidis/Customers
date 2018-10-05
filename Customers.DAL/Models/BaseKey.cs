using System.ComponentModel.DataAnnotations;

namespace Customers.DAL.Models
{
    /// <summary>
    /// Base Key Class
    /// </summary>
    public class BaseKey
    {
        [Key]
        public int Id { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace WebAPISample.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        [Required,Range(0, 10)]
        public decimal? Price { get; set; }
    }

}



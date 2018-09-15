
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPISample.Models
{
    public class Product
    {   
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        [Required, Range(0, 20)]
        public decimal? Price { get; set; }
        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }

}



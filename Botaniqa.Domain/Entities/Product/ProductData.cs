using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Botaniqa.Domain.Entities.Product
{
    public class ProductData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]

        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; } = 0;

        [Required]
        public string Image { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

    }
}
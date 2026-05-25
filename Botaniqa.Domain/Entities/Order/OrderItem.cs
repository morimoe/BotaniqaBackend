using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Botaniqa.Domain.Entities.Product;

namespace Botaniqa.Domain.Entities.Order
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("ProductId")]  // ← добавь это
        public ProductData? Product { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Botaniqa.Domain.Entities.Order
{
    public class OrderData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string House { get; set; } = string.Empty;

        [Required]

        public string? Apartment { get; set; }

        [Required]
        public string? Entrance { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Этаж должен быть больше нуля")]

        [Required]
        public string? Floor { get; set; }

        [Required]
        public string? Intercom { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string? Email { get; set; }
        public string? Comment { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItem> OrderItems { get; set; } = new();

    }
}
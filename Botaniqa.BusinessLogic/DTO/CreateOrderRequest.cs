using System.ComponentModel.DataAnnotations;

namespace Botaniqa.BL.OrderDTO
{
    public class CreateOrderRequest
    {
        [Required] public string City { get; set; } = string.Empty;
        [Required] public string Street { get; set; } = string.Empty;
        [Required] public string House { get; set; } = string.Empty;


        [Range(1, int.MaxValue, ErrorMessage = "Квартира должна быть больше нуля")]
        public string? Apartment { get; set; }
        public string? Entrance { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Этаж должен быть больше нуля")]
        public string? Floor { get; set; }
        public string? Intercom { get; set; }

        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }
        public string? Comment { get; set; }

        [Required] public string PaymentMethod { get; set; } = string.Empty;

        public List<OrderItemRequest> Items { get; set; } = new();
    }

    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
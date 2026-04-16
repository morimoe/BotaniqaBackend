namespace Botaniqa.Api.Domain
{////
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;    // "USD", "EUR", "MDL"
        public string Symbol { get; set; } = string.Empty;  // "$", "И", "L"
    }

    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } // всегда в MDL
        public bool IsAvailable { get; set; }
        public string? Image { get; set; }


        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;
    }
}
namespace Botaniqa.BL.ProductDTO
{

    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public int Stock { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class CreateProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public int Stock { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}

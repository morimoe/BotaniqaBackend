using System.ComponentModel.DataAnnotations;

namespace Botaniqa.BL.ProductDTO

{

    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
        public int Stock { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Название обязательно")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
        public int Stock { get; set; }

        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Категория обязательна")]
        public string Category { get; set; } = string.Empty;
    }

    public class UpdateProductRequest
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public string? Image { get; set; }
        public string? Category { get; set; }
    }
}

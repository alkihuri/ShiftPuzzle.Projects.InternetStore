using System.ComponentModel.DataAnnotations;

namespace MyStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        // Другие свойства, например, URL изображения, категория, количество на складе и т.д.
    }
}

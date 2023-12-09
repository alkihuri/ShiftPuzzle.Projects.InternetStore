using System.Collections.Generic;
using System.Linq;
using MyStore.Models;

namespace MyStore.Services
{
    public class ProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            // Инициализация с некоторыми тестовыми данными
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Продукт 1", Price = 100 },
                new Product { Id = 2, Name = "Продукт 2", Price = 200 },
                // Добавьте другие тестовые продукты здесь
            };
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            // В реальном приложении здесь должен быть код для добавления продукта в базу данных
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                // Обновление данных продукта
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                // Обновите другие свойства при необходимости
            }
        }

        public void DeleteProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                _products.Remove(existingProduct);
            }
        }
    }
}

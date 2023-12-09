using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using MyStore.Models;

namespace MyStore.Services
{
    public class ProductService
    {
        private readonly string _connectionString;

        public ProductService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("SELECT * FROM Products", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            // Остальные поля модели Product
                        });
                    }
                }
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("SELECT * FROM Products WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            // Остальные поля модели Product
                        };
                    }
                }
            }

            return null;
        }

        public async Task AddProductAsync(Product product)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("INSERT INTO Products (/* Список полей */) VALUES (/* Список значений */) ", connection);
                // Добавление параметров
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("UPDATE Products SET /* Список обновлений */ WHERE Id = @id", connection);
                // Добавление параметров
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SQLiteCommand("DELETE FROM Products WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

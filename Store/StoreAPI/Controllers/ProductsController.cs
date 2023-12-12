using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MyStore.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly string _filePath = "products.json";

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        var products = ReadFromFile();
        return Ok(products);
    }
  [HttpGet("random")]
    public ActionResult<IEnumerable<Product>> RandomFill()
    {
        var random = new Random();
        var products = ReadFromFile();

        for (int i = 0; i < 10; i++)
        {
            var product = new Product
            {
                Id = random.Next(1000, 9999), // Генерация случайного ID
                Name = "Product " + random.Next(1, 100), // Случайное название продукта
                Price = random.Next(10, 500) // Случайная цена
                // Добавьте другие необходимые поля
            };

            products.Add(product);
        }

        WriteToFile(products);

        return Ok(products);
    }

    // GET:  
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var products = ReadFromFile();
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

     // GET:  
    [HttpGet("GetByName/{name}")]
    public ActionResult<Product> GetProductByName(string name)
    {
        var products = ReadFromFile();
        var product = products.FirstOrDefault(p => p.Name == name);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> PostProduct(Product product)
    {
        var products = ReadFromFile();
        products.Add(product);
        WriteToFile(products);
        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }
    
     
    private List<Product> ReadFromFile()
    {
        if (!System.IO.File.Exists(_filePath))
        {
            return new List<Product>();
        }

        var json = System.IO.File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
    }

    private void WriteToFile(List<Product> products)
    {
        var json = JsonSerializer.Serialize(products);
        System.IO.File.WriteAllText(_filePath, json);
    }
}

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
  // GET:  
    [HttpGet("random")]
    public ActionResult<Product> RandomFiil()
    {
        var product = new Product();
        product.Id = 999;
        product.Name = "Test";
        var products = ReadFromFile();
        products.Add(product);
        WriteToFile(products);
        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
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

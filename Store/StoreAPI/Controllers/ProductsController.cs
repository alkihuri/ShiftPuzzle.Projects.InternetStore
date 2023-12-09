using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyStore.Models; 
using MyStore.Services;  



[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        var products = _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _productService.GetProductByIdAsync(id);

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
        _productService.AddProductAsync(product);
        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public IActionResult PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _productService.UpdateProductAsync(product);

        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _productService.DeleteProductAsync(id);

        return NoContent();
    }

  [HttpGet("random")]
public async Task<IActionResult> AddRandomProduct()
{
    // Генерация случайных данных (здесь просто для примера)
    var random = new Random();
    var product = new Product
    {
        Name = "Random Product " + random.Next(1, 1000),
        Description = "Random Description",
        Price = random.Next(10, 100)
    };

    // Добавление продукта в базу данных
    await _productService.AddProductAsync(product);

    return Ok(product);
}
    [HttpPost("add")]
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        await _productService.AddProductAsync(product);
        return Ok(product);
    }
}

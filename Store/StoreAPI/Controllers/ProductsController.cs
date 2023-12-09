using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyStore.Models; // Предполагается, что у вас есть модель Product
using MyStore.Services; // Предполагается, что у вас есть сервис ProductService



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
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _productService.GetProductById(id);

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
        _productService.AddProduct(product);
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

        _productService.UpdateProduct(product);

        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        _productService.DeleteProduct(product);

        return NoContent();
    }
}

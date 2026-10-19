using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;

public class ProductService : IProductService
{
    private readonly string _filePath = "products.json";
    private readonly List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Price = 22.0989m },
        new Product { Id = 2, Name = "Product 2", Price = 25.0987m },
        new Product { Id = 3, Name = "Product 3", Price = 77.03567m }
    };
      private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
        if (File.Exists(_filePath))
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                var productsFromFile = JsonSerializer.Deserialize<List<Product>>(json);
                if (productsFromFile is not null)
                {
                    _products.Clear();
                    _products.AddRange(productsFromFile);
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading products from file.");
            }
        }
    }
    private void SaveToFile()
    {
        string json = JsonSerializer.Serialize(_products);
        File.WriteAllText(_filePath, json);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _products;
    }

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product AddProduct(Product newProduct)
    {
        newProduct.Id = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;
        _products.Add(newProduct);
        SaveToFile();
        return newProduct;
    }

    public bool UpdateProduct(int id, Product updatedProduct)
    {
        var existingProduct = GetProductById(id);
        if (existingProduct is null)
            return false;

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
        SaveToFile();
        return true;
    }

    public bool DeleteProduct(int id)
    {
        var product = GetProductById(id);
        if (product is null)
            return false;

        _products.Remove(product);
        SaveToFile();
        return true;
    }
    public bool ValidateProduct(Product product)
    { 
        if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
    {
    return false;
    }
    return true;
     }
}
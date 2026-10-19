public class ProductService : IProductService
{
    private readonly List<Product> _products;

    public ProductService()
    {
        _products = new List<Product>
        {
            new Product { Name = "product1", Price = 22.0989m , Id = 1 },
            new Product { Name = "product2", Price = 25.0987m , Id = 2 },
            new Product { Name = "product3", Price = 77.03567m , Id = 3 }
        };
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _products;
    }

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void UpdateProduct(int id, Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
        }
    }

    public void DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }
}
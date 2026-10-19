public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    Product AddProduct(Product newProduct);
    bool UpdateProduct(int id, Product updatedProduct);
    bool DeleteProduct(int id);
    bool ValidateProduct(Product product);
}
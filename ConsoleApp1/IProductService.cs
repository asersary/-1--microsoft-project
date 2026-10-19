public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(int id, Product updatedProduct);
    void DeleteProduct(int id);
}
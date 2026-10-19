var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IProductService, ProductService>();
var app = builder.Build();
var products = new List<Product>
{
        new Product { Name = "product1", Price = 22.0989m , Id = 1 },
        new Product { Name = "product2", Price = 25.0987m , Id = 2 },
        new Product { Name = "product3", Price = 77.03567m , Id = 3 }
  
 };
app.MapGet("/",()=> "wellcome to my ApI");
app.MapGet( "/products", ()=> products);
app.MapGet( "/products/{id}",(int id) => {
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is null? Results.NotFound() : Results.Ok(product);
});
app.MapPost("/products", (Product newproduct) =>
{
    products.Add(newproduct);
    return Results.Created($"/products/{newproduct.Id}", newproduct); 
    
} );
app.MapPut("/products/{id}",(int id , Product updatedproduct) =>
{  var product = products.FirstOrDefault(p=>p.Id==id);
     if( product is null) return Results.NotFound();
     product.Name = updatedproduct.Name;
     product.Price = updatedproduct.Price;
     return Results.Ok(product); 
});
app.MapDelete("/products/{id}",(int id) =>
{
   var product = products.FirstOrDefault(p=>p.Id==id); 
   if(product is null) return Results.NotFound();
   products.Remove(product);
   return Results.NoContent();
});


    app.Run();


public class Product
{
 public int Id {get; set; }
 public string Name {get; set; } = string.Empty;
 public decimal Price {get; set; }

} 
using Microsoft.Extensions.Logging;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async(context,next)=>{
    try{
        await next();
    }        
    catch(Exception ex) {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Unhandled exception");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "something went wrong" });
    }
}); 
app.MapGet("/",()=> "Hello World!");
app.MapGet( "/products", (IProductService productService) => {
     return Results.Ok(productService.GetAllProducts());
     });
app.MapGet( "/products/{id}",(int id , IProductService productService) => {
    var product = productService.GetProductById(id);
    return product is null? Results.NotFound() : Results.Ok(product);
});
app.MapPost("/products", (Product newproduct, IProductService productService) =>
{ if (!productService.ValidateProduct(newproduct))
   {
    return Results.BadRequest("Invalid product data. Name cannot be empty and Price must be greater than 0.");
   }
   var createdProduct = productService.AddProduct(newproduct);
    return Results.Created($"/products/{createdProduct.Id}", createdProduct);
} );
app.MapPut("/products/{id}",(int id , Product updatedproduct , IProductService productService) =>
{   
    if (!productService.ValidateProduct(updatedproduct))
   {
    return Results.BadRequest("Invalid product data. Name cannot be empty and Price must be greater than 0.");
   }
    var success = productService.UpdateProduct(id, updatedproduct);
     if(!success) return Results.NotFound();
     return Results.NoContent(); 
});
app.MapDelete("/products/{id}",(int id, IProductService productService) =>
{
   var success = productService.DeleteProduct(id);
   if(!success) return Results.NotFound();
   return Results.NoContent();
});

app.Run();
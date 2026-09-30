using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
};

//Get all products from the JSON file
app.MapGet("/api/products", async (IWebHostEnvironment env) => 
{
    var filePath = Path.Combine(env.ContentRootPath, "data", "products.json");
    if (!File.Exists(filePath))
    {
        return Results.NotFound( new {error ="Products data file not found."});
    }

    await using var stream = File.OpenRead(filePath);
    var products = await JsonSerializer.DeserializeAsync<List<Product>>(stream, jsonOptions);

    return  Results.Ok(products) ;
});

//get items by id

app.MapGet("/api/products/{id:int}", async (int id, IWebHostEnvironment env) =>
{ 
    
    var filePath = Path.Combine(env.ContentRootPath, "data", "products.json");
    if (!File.Exists(filePath))
    {
        return Results.NotFound(new { error = "Products data file not found." });
    }

    await using var stream = File.OpenRead(filePath);
    var products = await JsonSerializer.DeserializeAsync<List<Product>>(stream, jsonOptions);

    var item = products?.FirstOrDefault(p => p.Id == id);
    if (item is null)
    {
        return Results.NotFound(new { error = $"Item with ID {id} not found." });
    }

    return Results.Ok(item);
});


app.Run();

//data model record
public record Product(int Id, string Name, decimal Price, bool InStock);
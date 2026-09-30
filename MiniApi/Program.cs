using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

//1.  Register swagger / OpenAPI services
builder.Services.AddEndpointsApiExplorer(); // Discovers endpoint routes and models
builder.Services.AddSwaggerGen();           // Generates the OpenAPI v3 spec 

var app = builder.Build();

// 2. Enable Swagger middleware in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    WriteIndented = true
};

//Get all products from the JSON file
app.MapGet("/api/products", async (IWebHostEnvironment env) => 
{
    var filePath = Path.Combine(env.ContentRootPath, "data", "products.json");
    if (!File.Exists(filePath))
    {
        return Results.NotFound( new {error ="Products data file not found."});
    }

    await using var filestream = File.OpenRead(filePath);
    var products = await JsonSerializer.DeserializeAsync<List<Product>>(filestream, jsonOptions);

    return  Results.Ok(products) ;
})
.WithName("GetAllProducts")
.WithSummary("Retrieves all products from the JSON data file.");

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
})
.WithName("GetProductById")
.WithSummary("Retrieves a single product by its numerical identifier.");;

//Post a new product to the JSON file
app.MapPost("/api/products", async (Product newProduct, IWebHostEnvironment env) =>
{
    var filePath = Path.Combine(env.ContentRootPath, "data", "products.json");

    List<Product> products = new();

    // 1. Read existing products if the file exists
    if (File.Exists(filePath))
    {
        await using var readStream = File.OpenRead(filePath);
        products = await JsonSerializer.DeserializeAsync<List<Product>>(readStream, jsonOptions) 
                   ?? new List<Product>();
    }

    // 2. Auto-generate next ID if not provided (or increment from highest)
    int nextId = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
    
    // Using C# record non-destructive mutation ('with' expression)
    var productToSave = newProduct with { Id = nextId };

    // 3. Append to list
    products.Add(productToSave);

    // 4. Write updated array back to disk
    await using var writeStream = File.Create(filePath);
    await JsonSerializer.SerializeAsync(writeStream, products, jsonOptions);

    // 5. Return 201 Created with Location header pointing to the new item
    return Results.Created($"/api/products/{productToSave.Id}", productToSave);
})
.WithName("CreateProduct")
.WithSummary("Creates a new product and persists it to the JSON file.");
app.Run();

//data model record
public record Product(int Id, string Name, decimal Price, bool InStock);
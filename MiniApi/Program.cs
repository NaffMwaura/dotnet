//Define the Data Model and In-Memory Store

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Next, initialize a simple in-memory list to serve as a mock data store:
//In Memory Data Store
var inventory = new List<Item>
{
    new Item(1, "Laptop", 999.99m, true),
    new Item(2, "Mouse", 25.50m, true),
    new Item(3, "Keyboard", 49.99m, false)
};

//get all items

app.MapGet("/api/items", () => Results.Ok(inventory));

//get items by id

app.MapGet("/api/items/{id:int}", (int id) =>
{
    var item = inventory.FirstOrDefault(i => i.Id == id);
    return item is not null ? Results.Ok(item) : Results.NotFound();
});

//POST new item

app.MapPost("/api/items", (Item newItem) =>
{
    var nextId = inventory.Max(i => i.Id) + 1;
    var itemToAdd = new Item(nextId, newItem.Name, newItem.Price, newItem.InStock);
    inventory.Add(itemToAdd);
    return Results.Created($"/api/items/{nextId}", itemToAdd);
});

//Delete item by id

app.MapDelete("/api/items/{id:int}", (int id) =>
{
    var item = inventory.FirstOrDefault(i => i.Id == id);
    if (item is null) return Results.NotFound();

    inventory.Remove(item);
    return Results.NoContent();
});

app.Run();

//data model record
public record Item(int Id, string Name, decimal Price, bool InStock);
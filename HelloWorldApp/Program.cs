// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Hello my name is Naff Zoe and I am here to learn C# programming language.");
string name = "Naff Zoe";
Console.WriteLine($"Hello as I said I am {name} and I am ready to change the world with my programming skills.");

int myAge = 21;
Console.WriteLine($"I am {myAge} years old and you already know my name {name} ");

int quantity = 3;
decimal unitPrice = 19.99m;
decimal taxRate = 0.07m;
bool isEligibleForDiscount = quantity > 3;

decimal totalPrice = quantity * unitPrice;
decimal discount = isEligibleForDiscount ?  10.00m : 0.00m;
decimal totalTax = (totalPrice - discount) * taxRate;

decimal finalPrice = totalPrice - discount + totalTax;

// Output the results
Console.WriteLine("--- ORDER RECEIPT ---");
Console.WriteLine($"Quantity: {quantity}");
Console.WriteLine($"Tax:      +${totalTax:F2}");
Console.WriteLine($"Final Price: {finalPrice:C}");


//Learning more about if/else statements and loops in C# programming language
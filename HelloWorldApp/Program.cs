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
decimal discount = isEligibleForDiscount ? 10.00m : 0.00m;
decimal totalTax = (totalPrice - discount) * taxRate;

decimal finalPrice = totalPrice - discount + totalTax;

// Output the results
Console.WriteLine("--- ORDER RECEIPT ---");
Console.WriteLine($"Quantity: {quantity}");
Console.WriteLine($"Tax:      +${totalTax:F2}");
Console.WriteLine($"Final Price: {finalPrice:C}");


//Learning more about if/else statements and loops in C# programming language
int accountBalance = 250;
bool isAccountActive = true;

if (isAccountActive)
{
    if (accountBalance > 500 && isAccountActive)
    {
        Console.WriteLine("Premier tier eligible.");
    }
    else if (accountBalance > 0 && isAccountActive)
    {
        Console.WriteLine("Standard active account.");
    }
    else
    {
        Console.WriteLine("Insufficient funds or inactive account.");
    }
}
// loops

for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Iteration index: {i}");
}

//Complete practical example

decimal[] orderAmounts = { 45.00m, 120.50m, 0.00m, 850.00m, -5.00m, 2100.00m };

Console.WriteLine("--- ORDER AUDIT REPORT ---");

foreach (decimal amount in orderAmounts)
{
    // 1. Guard check using simple if/else
    if (amount <= 0)
    {
        Console.WriteLine($"[FLAGGED] Invalid transaction amount: ${amount:F2}");
        continue; // Skip further processing for this item
    }

    // 2. Pattern matching switch expression
    string shippingTier = amount switch
    {
        < 50.00m => "Standard Shipping ($5.00)",
        >= 50.00m and < 200.00m => "Free Ground Shipping",
        >= 200.00m and < 1000.00m => "Priority Express Shipping",
        _ => "VIP White-Glove Courier"
    };

    // 3. Discount calculation with ternary operator
    decimal discount1 = amount >= 500.00m ? amount * 0.10m : 0.00m;
    decimal finalPayable = amount - discount1;

    Console.WriteLine($"Order: ${amount:F2} | Discount: -${discount1:F2} | Final: ${finalPayable:F2} | Tier: {shippingTier}");
}
        //Arrays in C# programming language

        int [] scores = new int[5];
        scores[0] = 85;
        scores[1] = 92;
        scores[2] = 78;
        scores[3] = 90;
        scores[4] = 88;
        Console.WriteLine("Scores:");
        foreach (int score in scores)
        {
            Console.WriteLine(score);
        }

        string[] fruits = { "Apple", "Banana", "Cherry", "Date", "Elderberry" };
        Console.WriteLine("Fruits:");

        string [] countries = ["USA", "Canada", "Germany", "France", "Japan"];
        Console.WriteLine("Countries:");

               // Objects and Classes in C# programming language
        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }        
            public string Email { get; set; }
            public bool IsActive { get; set; }

            //Constructor: Executes when creating a new instance of the class
            public User(int id, string username, string email, bool isActive)
            {
                Id = id;
                Username = username;
                Email = email;
                IsActive = true;
            }

            //methods: Define the behavior of the class
            public void Deactivate()
            {
                IsActive = false;
                Console.WriteLine($"User {Username} has been deactivated.");
            }
        }
    
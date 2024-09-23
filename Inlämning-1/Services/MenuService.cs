using Inlämning_1.Models;
using System.Globalization;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;


namespace Inlämning_1.Services;
internal static class MenuService
{
    private static readonly ProductService _productService = new();

    public static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Lägg till produkt:");
        Console.WriteLine("2. Lista ut alla produkter");
        Console.WriteLine("3. Ta bort en produkt:");
        Console.WriteLine("0. Avsluta");

        Console.WriteLine("Ange ett alternativ");
        var choice = MenuOptions(Console.ReadLine() ?? "");
        if (!choice)
        {
            Console.WriteLine("Ogiltlig inmatning");
            
        }
        Console.ReadKey();
    }
    public static bool MenuOptions(string selectedOption)
    {
        if (int.TryParse(selectedOption, out int option))
        {
            switch (option)
            {
                case 1:
                    AddProductMenu();
                    break;
                case 2:
                    ListAllProducts();
                    break;
                case 3:
                    RemoveProductMenu();
                    break;
                case 0:
                    ExitApp();
                    break;
                default:
                    return false;
            }
            return true;
        }
        return false;
    }
    public static void AddProductMenu()
    {
        var product = new Product();
        Console.Clear();

        Console.WriteLine("--- Lägg till vara ---");
        
        Console.Write("Ange produktnamn: ");
        while (true)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("sv-SE");
            product.ProductName = Console.ReadLine() ?? "";
            string validName = @"^[a-zA-ZåäöÅÄÖ]+$";
            
            if (Regex.IsMatch(product.ProductName, validName))
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("FEL");

            }    
        }

        Console.Write("Ange produktpris: ");
        while (true)
        {       
            decimal productPrice;
            if (decimal.TryParse(Console.ReadLine(), out productPrice))
            {  
                product.ProductPrice = productPrice;           
                break;
            }
            else
            {               
                Console.Clear();
                Console.WriteLine("Ange ett giltligt pris");    
            }
        }
        var response = _productService.AddProduct(product);
        Console.WriteLine(response.Message);
    }
    public static void ListAllProducts()
    {
        Console.Clear();
        Console.WriteLine("--- Produkter ---");
        var productsResponse = _productService.GetProducts();
        if (productsResponse.Succeeded && productsResponse != null)
        {
            foreach ( var product in productsResponse.Result)
            {
                Console.WriteLine($"{product.ProductName}: {product.ProductPrice}:- Id: {product.ProductId}");
            }
        }
        else
        {
            Console.WriteLine("Finns inga produkter");
        }
        Console.ReadKey(); 
    }
    public static void ExitApp()
    {
        Console.WriteLine("är du säker på att du vill avsluta? (y/n)");
        var answer = Console.ReadLine() ?? "";
        if (answer.ToLower() == "y")
        {
            Console.WriteLine("Program Avslutat");
            Environment.Exit(0);
        }
    }
    public static void RemoveProductMenu()
    {
        Console.WriteLine("--- Ta bort en produkt ---");

        Console.WriteLine("Ange produkt: ");
        
    }
}


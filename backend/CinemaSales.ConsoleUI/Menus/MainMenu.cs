using CinemaSales.ConsoleUI.Helpers;

namespace CinemaSales.ConsoleUI.Menus;

public class MainMenu
{
    public void Display()
    {
        ConsoleHelper.PrintHeader("CINEMA SALES MANAGEMENT SYSTEM");

        Console.WriteLine("1. Movies Management");
        Console.WriteLine("2. ShowTimes Management");
        Console.WriteLine("3. Purchase Ticket");
        Console.WriteLine("4. Purchase Snack");
        Console.WriteLine("5. Campaigns and Discounts");
        Console.WriteLine("6. Sales Reports");
        Console.WriteLine("0. Exit");
        Console.WriteLine(new string('=', 45));
    }

    public string GetUserChoice()
    {
        return InputHelper.ReadRequiredString("Select an option: ");
    }
}

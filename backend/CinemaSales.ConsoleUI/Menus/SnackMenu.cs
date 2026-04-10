using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Menus;

public class SnackMenu
{
    public Guid PromptSnackSelection(IEnumerable<SnackViewModel> snacks)
    {
        ConsoleHelper.PrintHeader("PURCHASE SNACKS");

        var list = snacks.ToList();

        if (!list.Any())
        {
            ConsoleHelper.PrintWarning("No snacks available.");
            InputHelper.Pause();
            return Guid.Empty;
        }

        for (int i = 0; i < list.Count; i++)
        {
            var snack = list[i];
            Console.WriteLine($"{i + 1}. {snack.Name} - {snack.Price:C}");
        }

        int choice = InputHelper.ReadInt("\nSelect a snack: ", 1, list.Count);
        return list[choice - 1].Id;
    }

    public int PromptQuantity()
    {
        return InputHelper.ReadInt("\nEnter quantity: ", 1, int.MaxValue);
    }

    public void DisplayPurchaseResult(string snackName, int quantity, decimal unitPrice, decimal totalPrice)
    {
        ConsoleHelper.PrintHeader("SNACK PURCHASE SUCCESSFUL");
        ConsoleHelper.PrintSuccess("Your snack purchase was completed.");
        Console.WriteLine();
        Console.WriteLine($"Snack: {snackName}");
        Console.WriteLine($"Quantity: {quantity}");
        Console.WriteLine($"Unit Price: {unitPrice:C}");
        Console.WriteLine($"Total Price: {totalPrice:C}");
        InputHelper.Pause();
    }

    public void DisplayError(string message)
    {
        ConsoleHelper.PrintError(message);
        InputHelper.Pause();
    }
}

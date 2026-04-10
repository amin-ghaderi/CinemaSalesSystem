using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Menus;

public class TicketMenu
{
    public Guid PromptShowTimeSelection(IEnumerable<ShowTimeViewModel> showTimes)
    {
        ConsoleHelper.PrintHeader("PURCHASE TICKET");

        var list = showTimes.ToList();

        if (!list.Any())
        {
            ConsoleHelper.PrintWarning("No showtimes available.");
            InputHelper.Pause();
            return Guid.Empty;
        }

        for (int i = 0; i < list.Count; i++)
        {
            var st = list[i];
            Console.WriteLine($"{i + 1}. {st.MovieTitle} - {st.StartTime:yyyy-MM-dd HH:mm} - {st.Price:C}");
        }

        int choice = InputHelper.ReadInt("\nSelect a showtime: ", 1, list.Count);
        return list[choice - 1].Id;
    }

    public int PromptTicketQuantity()
    {
        return InputHelper.ReadInt("\nEnter ticket quantity: ", 1, int.MaxValue);
    }

    public void DisplayPurchaseResult(TicketViewModel ticket)
    {
        ConsoleHelper.PrintHeader("PURCHASE SUCCESSFUL");
        ConsoleHelper.PrintSuccess("Your ticket purchase was completed.");
        Console.WriteLine();
        Console.WriteLine($"ShowTime ID: {ticket.ShowTimeId}");
        Console.WriteLine($"Quantity: {ticket.Quantity}");
        Console.WriteLine($"Unit Price: {ticket.UnitPrice:C}");
        Console.WriteLine($"Total Price: {ticket.TotalPrice:C}");
        InputHelper.Pause();
    }

    public void DisplayError(string message)
    {
        ConsoleHelper.PrintError(message);
        InputHelper.Pause();
    }
}

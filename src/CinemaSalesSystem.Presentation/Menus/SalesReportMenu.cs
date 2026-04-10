using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Menus;

public class SalesReportMenu
{
    public static void DisplayReport(SalesReportViewModel report)
    {
        ArgumentNullException.ThrowIfNull(report);

        ConsoleHelper.PrintHeader("SALES REPORT");
        Console.WriteLine($"Tickets Sold: {report.TicketsSold}");
        Console.WriteLine($"Snacks Sold: {report.SnacksSold}");
        Console.WriteLine($"Ticket Revenue: {report.TotalTicketRevenue:C}");
        Console.WriteLine($"Snack Revenue: {report.TotalSnackRevenue:C}");
        Console.WriteLine(new string('-', 45));
        ConsoleHelper.PrintSuccess($"Total Revenue: {report.TotalRevenue:C}");
        InputHelper.Pause("Press any key to return to the main menu...");
    }
}

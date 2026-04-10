using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Menus;

public class ShowTimeMenu
{
    public void DisplayShowTimes(IEnumerable<ShowTimeViewModel> showTimes)
    {
        ConsoleHelper.PrintHeader("SHOWTIMES");

        if (!showTimes.Any())
        {
            ConsoleHelper.PrintWarning("No showtimes available.");
            InputHelper.Pause("Press any key to return to the main menu...");
            return;
        }

        int index = 1;
        foreach (var showTime in showTimes)
        {
            Console.WriteLine($"{index}. {showTime.MovieTitle}");
            Console.WriteLine($"   Start Time: {showTime.StartTime:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"   Auditorium: {showTime.Auditorium}");
            Console.WriteLine($"   Price: {showTime.Price:C}");
            Console.WriteLine();
            index++;
        }

        InputHelper.Pause("Press any key to return to the main menu...");
    }
}

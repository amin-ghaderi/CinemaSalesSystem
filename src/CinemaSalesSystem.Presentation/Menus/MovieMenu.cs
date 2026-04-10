using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Menus;

public class MovieMenu
{
    public static void DisplayMovies(IEnumerable<MovieViewModel> movies)
    {
        ArgumentNullException.ThrowIfNull(movies);

        ConsoleHelper.PrintHeader("MOVIES");

        if (!movies.Any())
        {
            ConsoleHelper.PrintWarning("No movies available.");
            InputHelper.Pause("Press any key to return to the main menu...");
            return;
        }

        int index = 1;
        foreach (var movie in movies)
        {
            Console.WriteLine($"{index}. {movie.Title} ({movie.Genre})");
            Console.WriteLine($"   Duration: {movie.DurationInMinutes} minutes");
            Console.WriteLine($"   Rating: {movie.Rating}");
            Console.WriteLine();
            index++;
        }

        InputHelper.Pause("Press any key to return to the main menu...");
    }
}

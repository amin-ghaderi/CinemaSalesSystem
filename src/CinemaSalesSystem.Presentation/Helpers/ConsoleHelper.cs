namespace CinemaSales.ConsoleUI.Helpers;

public static class ConsoleHelper
{
    public static void PrintHeader(string title)
    {
        Console.Clear();
        Console.WriteLine(new string('=', 45));
        Console.WriteLine(title);
        Console.WriteLine(new string('=', 45));
    }

    public static void PrintSection(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {title} ---");
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ResetColor();
    }

    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

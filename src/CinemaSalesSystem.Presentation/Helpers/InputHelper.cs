namespace CinemaSales.ConsoleUI.Helpers;

public static class InputHelper
{
    public static int ReadInt(string message, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
        }
    }

    public static decimal ReadDecimal(string message, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Invalid input. Please enter a value between {min} and {max}.");
        }
    }

    public static string ReadRequiredString(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Input cannot be empty. Please try again.");
        }
    }

    public static Guid ReadGuid(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (Guid.TryParse(input, out Guid value))
            {
                return value;
            }

            Console.WriteLine("Invalid GUID format. Please try again.");
        }
    }

    public static void Pause(string message = "Press any key to continue...")
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.ReadKey();
    }
}

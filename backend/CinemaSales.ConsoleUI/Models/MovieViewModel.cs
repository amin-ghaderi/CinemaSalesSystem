namespace CinemaSales.ConsoleUI.Models;

public class MovieViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public string Rating { get; set; } = string.Empty;
}

namespace CinemaSales.ConsoleUI.Models;

public class ShowTimeViewModel
{
    public Guid Id { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public string Auditorium { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

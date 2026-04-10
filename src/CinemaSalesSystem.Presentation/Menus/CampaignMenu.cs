using CinemaSales.ConsoleUI.Helpers;
using CinemaSales.ConsoleUI.Models;

namespace CinemaSales.ConsoleUI.Menus;

public class CampaignMenu
{
    public static void DisplayCampaigns(IEnumerable<CampaignViewModel> campaigns)
    {
        ConsoleHelper.PrintHeader("CAMPAIGNS & DISCOUNTS");

        var list = campaigns.ToList();

        if (list.Count == 0)
        {
            ConsoleHelper.PrintWarning("No campaigns available.");
            InputHelper.Pause();
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            var campaign = list[i];
            Console.WriteLine($"{i + 1}. {campaign.Name}");
            Console.WriteLine($"   Description: {campaign.Description}");
            Console.WriteLine($"   Discount: {campaign.DiscountPercentage}%");
            Console.WriteLine();
        }

        InputHelper.Pause();
    }
}

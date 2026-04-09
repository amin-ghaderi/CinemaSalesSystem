namespace CinemaSales.Domain.Exceptions;

/// <summary>
/// Thrown when a campaign is no longer valid at the time of use.
/// </summary>
public sealed class ExpiredCampaignException : DomainException
{
    /// <summary>
    /// Initializes a new instance of <see cref="ExpiredCampaignException"/>.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ExpiredCampaignException(string message)
        : base(message)
    {
    }
}

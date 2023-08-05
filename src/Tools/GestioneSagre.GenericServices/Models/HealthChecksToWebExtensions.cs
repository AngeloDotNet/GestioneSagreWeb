namespace GestioneSagre.GenericServices.Models;

public class HealthChecksToWebExtensions
{
    public record HealthChecksWebExtensions(string UriWeb, string NameGroup, string[] TagsGroup, int PollingInterval);
}
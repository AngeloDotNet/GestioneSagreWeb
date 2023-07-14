namespace GestioneSagre.GenericServices.Models;

public class HealthChecksToDatabaseExtensions
{
    public record HealthChecksDatabaseExtensions(string DbConnection, string NameGroup, string[] TagsGroup, int PollingInterval);
}
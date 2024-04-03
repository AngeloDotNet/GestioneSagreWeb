namespace GestioneSagre.Messaging.Extensions;

internal static class RabbitStaticConfig
{
    internal static bool durable = true;
    internal static bool autoDelete = false;

    internal static string exchangeType = "fanout";

    internal static int prefetchCount = 5;
    internal static int retryCount = 3;
    internal static int interval = 5000;
    internal static int queueExpiration = 5;
}
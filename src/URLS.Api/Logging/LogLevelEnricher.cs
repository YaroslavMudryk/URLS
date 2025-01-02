using Serilog.Core;
using Serilog.Events;

namespace URLS.Api.Logging;

public class LogLevelEnricher : ILogEventEnricher
{
    private const string LogLevelLogProperty = "Level";

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        ArgumentNullException.ThrowIfNull(logEvent);
        ArgumentNullException.ThrowIfNull(propertyFactory);

        var logLevel = propertyFactory.CreateProperty(LogLevelLogProperty, logEvent.Level);
        logEvent.AddPropertyIfAbsent(logLevel);
    }
}

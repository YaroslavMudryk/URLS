using Serilog.Core;
using Serilog.Events;

namespace URLS.Api.Logging;

public class RemovePropertiesEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        ArgumentNullException.ThrowIfNull(logEvent);

        logEvent.RemovePropertyIfPresent("RequestId");
        logEvent.RemovePropertyIfPresent("RequestPath");
        logEvent.RemovePropertyIfPresent("ConnectionId");
    }
}
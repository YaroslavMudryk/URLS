using Serilog.Core;
using Serilog.Events;

namespace URLS.Api.Logging;

public class ExcludeNullPropertiesPolicy : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
    {
        var properties = value?.GetType()
            .GetProperties()
            .Where(prop => prop.GetValue(value) != null)
            .Select(prop => new LogEventProperty(prop.Name, propertyValueFactory.CreatePropertyValue(prop.GetValue(value))));

        result = new StructureValue(properties!);
        return true;
    }
}

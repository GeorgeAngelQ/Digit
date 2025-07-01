using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Libreria;
public static class JsonPatchInput
{
    public static NewtonsoftJsonPatchInputFormatter GetNewtonsoft()
    {
        var builder = new ServiceCollection()
            .AddLogging()
            .AddMvc()
            .AddNewtonsoftJson()
            .Services.BuildServiceProvider();

        return builder
            .GetRequiredService<IOptions<MvcOptions>>()
            .Value
            .InputFormatters
            .OfType<NewtonsoftJsonPatchInputFormatter>()
            .First();
    }

    public static bool PropertyBool<T>(JsonPatchDocument<T> objectPatch, string propertyName) where T : class
    {
        string rawValue;
        bool exists;
        var operations = objectPatch.Operations;

        exists = operations.Any(o => o.path == propertyName);
        if (exists)
        {
            rawValue = operations.FirstOrDefault(p => p.path == propertyName)?.value.ToString();
        }
        else
        {
            rawValue = operations.FirstOrDefault(p => p.path.Substring(1) == propertyName)?.value.ToString();
        }

        var propValue = Conversiones.ToType(typeof(bool), rawValue);

        return propValue;
    }

    public static int? PropertyNullableInt32<T>(JsonPatchDocument<T> objectPatch, string propertyName) where T : class
    {
        string? rawValue;
        bool exists;
        var operations = objectPatch.Operations;

        exists = operations.Any(o => o.path == propertyName);
        if (exists)
        {
            rawValue = operations.FirstOrDefault(p => p.path == propertyName)?.value.ToString();
        }
        else
        {
            rawValue = operations.FirstOrDefault(p => p.path.Substring(1) == propertyName)?.value.ToString();
        }

        if (rawValue == null || rawValue == "null")
        {
            return null;
        }

        var propValue = Conversiones.ToType(typeof(int), rawValue);

        return propValue;
    }

    public static string? PropertyString<T>(JsonPatchDocument<T> objectPatch, string propertyName) where T : class
    {
        bool exists;
        var operations = objectPatch.Operations;

        exists = operations.Any(o => o.path == propertyName);
        if (exists)
        {
            return operations.FirstOrDefault(p => p.path == propertyName)?.value.ToString();
        }

        return operations.FirstOrDefault(p => p.path.Substring(1) == propertyName)?.value.ToString();
    }
}
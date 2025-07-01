using System.Net.Http.Headers;


namespace Libreria;

public static class MethodsHeaders
{
    public const string ExistsObject = "exists-object";

    public static string ControllerName(string nameClass)
    {
        if (nameClass.EndsWith("Controller"))
        {
            return nameClass.Substring(0, nameClass.Length - "Controller".Length).ToLower();
        }

        return nameClass;
    }

    public static bool HeaderBoolValue(HttpResponseHeaders headers, string headerKey)
    {
        var itemExists = headers.FirstOrDefault(k => k.Key == headerKey).Value.ToList();
        var nullableValue = Conversiones.ToNullableBool(itemExists[0]);

        return nullableValue ?? false;
    }
}
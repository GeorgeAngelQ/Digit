namespace Libreria
{
    public static class Conversiones
    {
        public static bool? ToNullableBool(string value)
        {
            bool? bValue = null;

            if (value == null || value == "null")
            {
                bValue = null;
            }
            else
            {
                bValue = bool.Parse(value);
            }

            return bValue;
        }

        public static int? ToNullableInt32(string value)
        {
            if (value is null or "null")
            {
                return null;
            }
            _ = int.TryParse(value, out var integerValue);

            return integerValue;
        }

        public static dynamic ToType(Type T, string value)
        {
            try
            {
                if (T == typeof(bool))
                {
                    if (value == null || value == "null")
                    {
                        return default(bool);
                    }

                    return bool.Parse(value);
                }
                if (T == typeof(int))
                {
                    if (value == null || value == "null")
                    {
                        return default(int);
                    }
                    return int.Parse(value);
                }

                return value;
            }
            catch
            {
                return default(bool);
            }
        }
    }
}

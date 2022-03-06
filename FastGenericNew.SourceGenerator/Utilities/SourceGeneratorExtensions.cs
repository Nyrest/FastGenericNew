namespace FastGenericNew.SourceGenerator.Utilities;

internal static class SourceGeneratorExtensions
{
    public const string prefix = "FastNew_";

    public static bool TryGetProperty(this AnalyzerConfigOptions options, string name, out string? value)
    {
        return options.TryGetValue($"build_property.{prefix}{name}", out value);
    }

    public static TValue GetOrDefault<TValue>(this AnalyzerConfigOptions? options, string name, TValue defaultValue)
    {
        if (options is null) return defaultValue;
        Unsafe.SkipInit(out TValue result);

        if (TryGetProperty(options, name, out var rawValue))
        {
            // Double check to solve IntelliSense nullable warning.
            if (rawValue is null || string.IsNullOrEmpty(rawValue)) return defaultValue;
            var type = typeof(TValue);

            if (rawValue == "%EMPTY%")
                rawValue = string.Empty;

            if (type == typeof(int))
            {
                if (int.TryParse(rawValue, out Unsafe.As<TValue, int>(ref result)))
                    return result;
            }
            else if (type == typeof(bool))
            {
                if (bool.TryParse(rawValue, out Unsafe.As<TValue, bool>(ref result)))
                    return result;

                #region Fallback

                return rawValue.Trim().ToLower() switch
                {
                    "enable" or
                    "enabled" or
                    "yes" or
                    "1" => (TValue)(object)true,

                    "disable" or
                    "disabled" or
                    "no" or
                    "0" or
                    "-1" => (TValue)(object)false,

                    _ => defaultValue,
                };
                #endregion
            }
            else if (type == typeof(string))
            {
                return (TValue)(object)rawValue;
            }
            else if (type.IsEnum)
            {
                return (TValue)Enum.Parse(typeof(TValue), rawValue, true);
            }
            else throw new InvalidOperationException($"Unexpected Type: {type}");
        }
        return defaultValue;
    }
}
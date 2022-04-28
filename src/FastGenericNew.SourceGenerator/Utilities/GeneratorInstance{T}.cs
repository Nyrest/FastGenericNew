namespace FastGenericNew.SourceGenerator.Utilities;

public static class GeneratorInstance<T> where T : CodeGenerator, new()
{
    public static readonly T Instance = new();

    private static CodeGenerationResult? _cachedResult;

    public static CodeGenerationResult Generate(in GeneratorOptions _oldOptions, in GeneratorOptions options)
    {
        return !Instance.ShouldUpdate(in _oldOptions, in options) && _cachedResult != null
            ? _cachedResult
            : (_cachedResult = Instance.Generate(in options));
    }
}

namespace FastGenericNew.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public unsafe class Generator : IIncrementalGenerator
{
    private static GeneratorOptions _lastOptions;

    private static readonly nint[] generatorPointers = Assembly
        .GetCallingAssembly()
        .GetTypes()
        .Where(static x => !x.IsAbstract && typeof(CodeGenerator).IsAssignableFrom(x))
        .Select(static x =>
            (nint)
            typeof(GeneratorInstance<>)
            .MakeGenericType(x)
            .GetMethod(nameof(GeneratorInstance<FastNewCoreGenerator>.Generate))
            .MethodHandle.GetFunctionPointer()
            )
        .ToArray();

    private static readonly object _lock = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.AnalyzerConfigOptionsProvider.WithComparer(AnalyzerConfigComparer.Instance), BuildSource);
        static void BuildSource(SourceProductionContext sourceContext, AnalyzerConfigOptionsProvider optionsProvider)
        {
            var newOptions = new GeneratorOptions(optionsProvider);

            if (newOptions.DisableGeneratorCache)
            {
                _lastOptions = default;
            }

            if (newOptions.MultiThreadedGeneration)
            {
                Parallel.ForEach(generatorPointers, nativePointer =>
                {
                    var function = (delegate* managed<in GeneratorOptions, in GeneratorOptions, CodeGenerationResult>)nativePointer;
                    var result = function(in _lastOptions, in newOptions);
                    if (result.SourceText != null)
                    {
                        lock (_lock)
                        {
                            sourceContext.AddSource(result.Filename, result.SourceText);
                        }
                    }
                    if (result.Diagnostics != null)
                    {
                        lock (_lock)
                        {
                            foreach (Diagnostic diag in result.Diagnostics)
                            {
                                sourceContext.ReportDiagnostic(diag);
                            }
                        }
                    }
                });
            }
            else
            {
                foreach (var nativePointer in generatorPointers)
                {
                    var function = (delegate* managed<in GeneratorOptions, in GeneratorOptions, CodeGenerationResult>)nativePointer;
                    var result = function(in _lastOptions, in newOptions);
                    if (result.SourceText != null)
                        sourceContext.AddSource(result.Filename, result.SourceText);
                    if (result.Diagnostics != null)
                    {
                        foreach (Diagnostic diag in result.Diagnostics)
                        {
                            sourceContext.ReportDiagnostic(diag);
                        }
                    }
                }
            }
            _lastOptions = newOptions;
        }
    }

    class AnalyzerConfigComparer : IEqualityComparer<AnalyzerConfigOptionsProvider>
    {
        public static readonly AnalyzerConfigComparer Instance = new();

        public bool Equals(AnalyzerConfigOptionsProvider x, AnalyzerConfigOptionsProvider y) =>
            new GeneratorOptions(x).Equals(new GeneratorOptions(y));

        public int GetHashCode(AnalyzerConfigOptionsProvider obj) =>
            new GeneratorOptions(obj).GetHashCode();
    }
}

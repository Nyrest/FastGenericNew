namespace FastGenericNew.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public unsafe class Generator : IIncrementalGenerator
{
    private static readonly CodeGenerator[] generators = Assembly
        .GetCallingAssembly()
        .GetTypes()
        .Where(static x => !x.IsAbstract && typeof(CodeGenerator).IsAssignableFrom(x))
        .Select(static x => (CodeGenerator)Activator.CreateInstance(x))
        .ToArray();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        foreach (var generator in generators)
        {
            var comparer = new AnalyzerConfigComparer(generator);
            context.RegisterSourceOutput(context.AnalyzerConfigOptionsProvider.WithComparer(comparer), (SourceProductionContext sourceContext, AnalyzerConfigOptionsProvider optionsProvider) =>
            {
                var options = new GeneratorOptions(optionsProvider);
                var result = generator.Generate(in options);
                if (result.SourceText != null)
                    sourceContext.AddSource(result.Filename, result.SourceText);
                if (result.Diagnostics != null)
                {
                    foreach (Diagnostic diag in result.Diagnostics)
                    {
                        sourceContext.ReportDiagnostic(diag);
                    }
                }
            });
        }
    }

    class AnalyzerConfigComparer : IEqualityComparer<AnalyzerConfigOptionsProvider>
    {
        private readonly CodeGenerator generator;

        public AnalyzerConfigComparer(CodeGenerator generator)
        {
            this.generator = generator;
        }

        public bool Equals(AnalyzerConfigOptionsProvider x, AnalyzerConfigOptionsProvider y) =>
           generator.GetOptionsSubset(new GeneratorOptions(x)).Equals(generator.GetOptionsSubset(new GeneratorOptions(y)));

        public int GetHashCode(AnalyzerConfigOptionsProvider obj) =>
            generator.GetOptionsSubset(new GeneratorOptions(obj)).GetHashCode();
    }
}

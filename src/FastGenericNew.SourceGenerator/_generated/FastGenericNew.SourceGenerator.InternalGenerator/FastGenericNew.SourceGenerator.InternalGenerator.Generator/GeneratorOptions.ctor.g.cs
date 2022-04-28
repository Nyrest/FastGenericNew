#nullable enable

namespace FastGenericNew.SourceGenerator;

partial record struct GeneratorOptions
{
    public GeneratorOptions(AnalyzerConfigOptionsProvider? provider)
    {
        var options = provider?.GlobalOptions;
        MaxParameterCount = options.GetOrDefault(nameof(MaxParameterCount), 16);
        PublicFastNewCore = options.GetOrDefault(nameof(PublicFastNewCore), false);
        GenerateTryCreateInstance = options.GetOrDefault(nameof(GenerateTryCreateInstance), true);
        GenerateCreateInstance = options.GetOrDefault(nameof(GenerateCreateInstance), true);
        GenerateTypeCreateInstance = options.GetOrDefault(nameof(GenerateTypeCreateInstance), true);
        NonPublicConstructorSupport = options.GetOrDefault(nameof(NonPublicConstructorSupport), true);
        Namespace = options.GetOrDefault(nameof(Namespace), "FastGenericNew");
        ForceFastNewDelegate = options.GetOrDefault(nameof(ForceFastNewDelegate), false);
        AlertGeneratedFile = options.GetOrDefault(nameof(AlertGeneratedFile), true);
        DisableGeneratorCache = options.GetOrDefault(nameof(DisableGeneratorCache), false);
        PrettyOutput = options.GetOrDefault(nameof(PrettyOutput), false);
        MultiThreadedGeneration = options.GetOrDefault(nameof(MultiThreadedGeneration), true);
        OutputGenerationInfo = options.GetOrDefault(nameof(OutputGenerationInfo), false);
        AllowUnsafeImplementation = options.GetOrDefault(nameof(AllowUnsafeImplementation), false);
    }
}
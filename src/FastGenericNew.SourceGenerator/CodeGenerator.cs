namespace FastGenericNew.SourceGenerator;

public abstract partial class CodeGenerator
{
    public abstract string Filename { get; }

    public virtual bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue) =>
        oldValue.Namespace != newValue.Namespace
        || oldValue.MaxParameterCount != newValue.MaxParameterCount
        || oldValue.AlertGeneratedFile != newValue.AlertGeneratedFile
        || oldValue.PrettyOutput != newValue.PrettyOutput
        || PreProcessorRelatedCheck(in oldValue, in newValue);

    public abstract CodeGenerationResult Generate(in GeneratorOptions options);

    private static partial bool PreProcessorRelatedCheck(in GeneratorOptions oldValue, in GeneratorOptions newValue);
}

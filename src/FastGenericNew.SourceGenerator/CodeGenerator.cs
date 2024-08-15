namespace FastGenericNew.SourceGenerator;

public abstract partial class CodeGenerator
{
    public abstract string Filename { get; }

    public abstract CodeGenerationResult Generate(in GeneratorOptions options);

    public virtual GeneratorOptions GetOptionsSubset(GeneratorOptions options)
    {
        return new GeneratorOptions() with
        {
            Namespace = options.Namespace,
            MaxParameterCount = options.MaxParameterCount,
            AlertGeneratedFile = options.AlertGeneratedFile,
            PrettyOutput = options.PrettyOutput
        };
    }
}

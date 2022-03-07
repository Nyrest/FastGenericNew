namespace FastGenericNew.SourceGenerator;

public abstract class CodeGenerator
{
    public abstract string Filename { get; }

    public virtual bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue) => 
        oldValue.Namespace != newValue.Namespace
        || oldValue.MaxParameterCount != newValue.MaxParameterCount
        || oldValue.AlertGeneratedFile != newValue.AlertGeneratedFile
        || oldValue.PrettyOutput != newValue.PrettyOutput;

    public abstract CodeGenerationResult Generate(in GeneratorOptions options);
}

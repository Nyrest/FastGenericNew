namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class TypeNewGenerator : CodeGenerator<TypeNewGenerator>
{
    public override string Filename => "FastNew.CreateInstance.g.cs";

    internal const string ClassName = "FastNew";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        return CodeGenerationResult.Empty;
    }
}
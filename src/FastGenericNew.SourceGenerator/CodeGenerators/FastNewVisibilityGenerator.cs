namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class FastNewVisibilityGenerator : CodeGenerator<FastNewVisibilityGenerator>
{
    public override string Filename => "FastNewVisibility.g.cs";

    internal const string ClassName = "FastNew";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(1024, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.Indent(1);

        builder.AppendKeyword(options.PublicFastNew ? "public" : "internal");

        builder.AppendKeyword("static partial class");
        builder.Append(ClassName);
        builder.StartBlock(1);
        builder.EndBlock(1);
        builder.EndNamespace();

        return builder.BuildAndDispose(this);
    }

    public override GeneratorOptions GetOptionsSubset(GeneratorOptions options)
    {
        return base.GetOptionsSubset(options) with
        {
            PublicFastNew = options.PublicFastNew,
        };
    }
}
namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class FastNewDelegateGenerator : CodeGenerator<FastNewDelegateGenerator>
{
    public override string Filename => "FastNewDelegate.g.cs";

    internal const string ClassName = "FastNew";

    internal const string DelegateName = "FastNewDelegate";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        if (!options.ForceFastNewDelegate && options.MaxParameterCount <= 16)
            return CodeGenerationResult.Empty;

        CodeBuilder builder = new(options.ForceFastNewDelegate ? 20480 : 1024 * options.MaxParameterCount - 16, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.Indent(1);
        builder.AppendKeyword("static partial class");
        builder.Append(ClassName);

        builder.StartBlock(1);
        for (int parameterIndex = options.ForceFastNewDelegate ? 0 : 17; parameterIndex <= options.MaxParameterCount; parameterIndex++)
        {
            builder.Indent(2);
            builder.Append($"public delegate T {DelegateName}");
            builder.DeclareFullGenericDelegate(parameterIndex);
            builder.PrettyNewLine();
        }
        builder.EndBlock(1);
        builder.EndNamespace();

        return builder.BuildAndDispose(this);
    }

    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue) =>
        base.ShouldUpdate(oldValue, newValue)
        || oldValue.ForceFastNewDelegate != newValue.ForceFastNewDelegate;
}

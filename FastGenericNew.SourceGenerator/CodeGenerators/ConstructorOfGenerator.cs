namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class ConstructorOfGenerator : CodeGenerator<ConstructorOfGenerator>
{
    public override string Filename => "ConstructorOf.g.cs";

    internal const string ClassName = "FastNew";

    internal const string ValueName = "Constructor";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(20480, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();

        for (int parameterIndex = 0; parameterIndex <= options.MaxParameterCount; parameterIndex++)
        {
            builder.Indent(1);
            builder.AppendAccessibility(options.PublicFastNewCore);
            builder.AppendKeyword("partial class");

            builder.Append(ClassName);
            builder.DeclareGenericMember(parameterIndex, ClassName == "FastNew");
            builder.AppendLine();

            builder.StartBlock(1);

            builder.XmlDoc(2, @"
/// <summary>
/// The constructor of <typeparamref name=""T"" /> with given arguments. <br/>
/// Could be <see langword=""null"" /> if the constructor couldn't be found.
/// </summary>");

            builder.Indent(2);
            builder.AppendAccessibility(options.FastNew_PublicConstructorCache);
            builder.Append($"static readonly ConstructorInfo? {ValueName} = typeof(T).GetConstructor(");
            builder.Append(options.NonPublicConstructorSupport
                ? "BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic"
                : "BindingFlags.Instance | BindingFlags.Public");
            builder.Append(", null, ");
            if (parameterIndex == 0)
            {
                builder.Append("Type.EmptyTypes");
            }
            else
            {
                builder.Append("new Type[]");
                builder.AppendLine();
                builder.StartBlock(2);

                for (int i = 0; i < parameterIndex; i++)
                {
                    builder.Indent(3);
                    builder.Append("typeof(");
                    builder.AppendGenericArgumentName(i);
                    builder.Append(')', ',');
                    builder.AppendLine();
                }

                builder.EndBlock(2, false);
            }
            builder.AppendLine(", null);");

            builder.EndBlock(1);
        }

        builder.EndNamespace();

        return builder.BuildAndDispose(this);
    }

    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue) =>
        base.ShouldUpdate(oldValue, newValue)
        || oldValue.FastNew_PublicConstructorCache != newValue.FastNew_PublicConstructorCache;
}
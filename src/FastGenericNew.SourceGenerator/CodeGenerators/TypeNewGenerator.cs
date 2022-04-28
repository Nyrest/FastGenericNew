namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class TypeNewGenerator : CodeGenerator<TypeNewGenerator>
{
    public override string Filename => "TypeNew.CreateInstance.g.cs";

    internal const string ClassName = "FastNew";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(20480, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();

        builder.AppendKeyword("public static partial class");
        builder.Append(ClassName);

        builder.StartBlock(1);

        for (int parameterIndex = 0; parameterIndex <= options.MaxParameterCount; parameterIndex++)
        {
            builder.Indent(2);
            builder.AppendKeyword("public static");
            builder.UseGenericDelegate(parameterIndex);
            builder.Append(" GetCreateInstance");
            builder.DeclareGenericMember(parameterIndex);
            #region MyRegion
            builder.Append("(Type type");
            for (int i = 0; i < parameterIndex; i++)
            {
                builder.Append(", Type ");
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.AppendLine(')');

            builder.StartBlock(2);
            #region Body

            builder.Append(3, "var result = typeof(");
            builder.Append(FastNewCoreGenerator.ClassName);
            builder.Append('<');
            builder.Append(',', parameterIndex);
            builder.Append(">).MakeGenericType(type");
            for (int i = 0; i < parameterIndex; i++)
            {
                builder.Append(',', ' ');
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.AppendLine($").GetField(\"{FastNewCoreGenerator.CompiledDelegateName}\")!.GetValue(null)!;");
            builder.Append(3, "return global::System.Runtime.CompilerServices.Unsafe.As<object, ");
            builder.UseGenericDelegate(parameterIndex);
            builder.AppendLine(">(ref result);");

            #endregion
            builder.EndBlock(2);

            #endregion
            builder.PrettyNewLine();
        }

        builder.EndBlock(1);

        builder.EndNamespace();
        return builder.BuildAndDispose(this);
    }
}
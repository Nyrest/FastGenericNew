namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class TypeNewGenerator : CodeGenerator<TypeNewGenerator>
{
    public override string Filename => "TypeNew.CreateInstance.g.cs";

    internal const string ClassName = "FastNew";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(65536, in options);
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
            builder.AppendLine(") =>");
            builder.Indent(3);
            builder.Append('(');
            builder.UseGenericDelegate(parameterIndex);

            builder.Append(")typeof(");
            builder.Append(FastNewCoreGenerator.ClassName);
            builder.Append('<');
            builder.Append(',', parameterIndex);
            builder.Append(">).MakeGenericType(type");
            for (int i = 0; i < parameterIndex; i++)
            {
                builder.Append(',', ' ');
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.Append($").GetField(\"{FastNewCoreGenerator.CompiledDelegateName}\").GetValue(null);");
            #endregion
            builder.PrettyNewLine();
        }

        builder.EndBlock(1);

        builder.EndNamespace();
        return builder.BuildAndDispose(this);
    }
}
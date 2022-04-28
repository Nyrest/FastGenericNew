namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class FastTryCreateInstanceGenerator : CodeGenerator<FastTryCreateInstanceGenerator>
{
    public override string Filename => "FastNew.TryCreateInstance.g.cs";

    internal const string ClassName = "FastNew";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        if (!options.GenerateTryCreateInstance) return CodeGenerationResult.Empty;
        CodeBuilder builder = new(20480, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.Indent(1);
        builder.AppendKeyword("public static partial class");
        builder.Append(ClassName);

        builder.StartBlock(1);

        builder.AppendLine($@"
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static bool TryCreateInstance<
#if NET5_0_OR_GREATER
{options.DynamicallyAccessedMembers(0)}
#endif
T>(out T result)
	    {{
            if({options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.IsValidName})
            {{
#if NETFRAMEWORK
                result = {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.CompiledDelegateName}();
#else
		        result = typeof(T).IsValueType
                    ? System.Activator.CreateInstance<T>()
                    : {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.CompiledDelegateName}();
#endif
                return true;
            }}
	        //Unsafe.SkipInit<T>(out result);
            result = default!;
	        return false;
	    }}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static bool TryNewOrDefault<
#if NET5_0_OR_GREATER
{options.DynamicallyAccessedMembers(0)}
#endif
T>(out T result)
	    {{
            if({options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.IsValidName})
            {{
#if NETFRAMEWORK
		        result = {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.IsValueTypeTName}
#else
    		    result = typeof(T).IsValueType
#endif
                    ? default(T)! // This will never be null since T is a ValueType
                    : FastNew<T>.CompiledDelegate();
                    return true;
            }}
	        //Unsafe.SkipInit<T>(out result);
            result = default!;
	        return false;
	    }}");

        for (int parameterIndex = 1; parameterIndex <= options.MaxParameterCount; parameterIndex++)
        {
            builder.Indent(2);
            builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.Indent(2);
            builder.Append("public static bool TryCreateInstance");
            builder.DeclareGenericMember(parameterIndex);
            #region MyRegion
            builder.Append('(');
            for (int i = 0; i < parameterIndex; i++)
            {
                builder.AppendGenericArgumentName(i);
                builder.Append(' ');
                builder.AppendGenericMethodArgumentName(i);
                builder.Append(',', ' ');
            }
            builder.AppendLine($"out T result)");
            builder.StartBlock(2);

            builder.Indent(3);
            builder.Append($"if ({options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}");
            builder.UseGenericMember(parameterIndex);
            builder.AppendLine($".{FastNewCoreGenerator.IsValidName})");

            builder.StartBlock(3);
            builder.Indent(4);
            builder.Append($"result = {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}");
            builder.UseGenericMember(parameterIndex);
            builder.Append($".{FastNewCoreGenerator.CompiledDelegateName}(");
            for (int i = 0; i < parameterIndex; i++)
            {
                if (i != 0)
                    builder.Append(',', ' ');
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.Append(')', ';');
            builder.AppendLine();
            builder.Indent(4);
            builder.AppendLine("return true;");
            builder.EndBlock(3);

            builder.Indent(3);
            //builder.AppendLine("Unsafe.SkipInit(out result);");
            builder.Append("result = default!;");
            builder.Indent(3);
            builder.AppendLine("return false;");
            builder.EndBlock(2);

            #endregion
            builder.PrettyNewLine();
        }

        builder.EndBlock(1);
        builder.EndNamespace();

        return builder.BuildAndDispose(this);
    }

    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue)
    {
        return base.ShouldUpdate(oldValue, newValue) && newValue.GenerateTryCreateInstance;
    }
}

namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class FastCreateInstanceGenerator : CodeGenerator<FastCreateInstanceGenerator>
{
    public override string Filename => "FastNew.CreateInstance.g.cs";

    internal const string ClassName = "FastNew";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        if (!options.GenerateCreateInstance) return CodeGenerationResult.Empty;
        CodeBuilder builder = new(20480, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.Indent(1);
        builder.AppendKeyword("static partial class");
        builder.Append(ClassName);
        builder.StartBlock(1);
        builder.AppendLine($@"
        /// <summary>
        /// <para>Create an instance of <typeparamref name=""T"" /></para>
        /// <para>Returns <c><see langword=""new"" /> <typeparamref name=""T"" />()</c> if <typeparamref name=""T""/> is a <see cref=""ValueType""/>(struct)</para>
        /// <para>This <b>CAN</b> call the Parameterless Constructor for <see cref=""ValueType""/>(struct)</para>
        /// </summary>
        /// <typeparam name=""T"">The type to create.</typeparam>
        /// <returns>A new instance of <typeparamref name=""T"" /></returns>
        /// <remarks>
        /// Equivalent to <c><see langword=""new"" /> <typeparamref name=""T"" />()</c> for both Reference Types and Value Types
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static T CreateInstance<
#if NET5_0_OR_GREATER
{options.DynamicallyAccessedMembers(0)}
#endif
T>()
	    {{
#if NETFRAMEWORK
            return {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.CompiledDelegateName}();
#else
		    return typeof(T).IsValueType
                ? System.Activator.CreateInstance<T>() // Value Types will be optimized by JIT in CoreCLR

    #if {ClrAllocatorGenerator.ppEnabled}
                : ({options.GlobalNSDot()}{ClrAllocatorGenerator.ClassName}<T>.IsSupported
                    ? {options.GlobalNSDot()}{ClrAllocatorGenerator.ClassName}<T>.CreateInstance()
                    : {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.CompiledDelegateName}());
    #else
                : {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.CompiledDelegateName}();
    #endif

#endif
	    }}
        /// <summary>
        /// Create an instance of <typeparamref name=""T"" /> <br/>
        /// Returns <c><see langword=""default"" />(<typeparamref name=""T"" />)</c> if <typeparamref name=""T""/> is a <see cref=""ValueType""/>(struct) <br/>
        /// This <b>WILL NOT</b> call the Parameterless Constructor for <see cref=""ValueType""/>(struct)
        /// </summary>
        /// <typeparam name=""T"">The type to create.</typeparam>
        /// <returns>A new instance of <typeparamref name=""T"" /></returns>
        /// <remarks>
        /// For reference types, equivalent to <c><see langword=""new"" /> <typeparamref name=""T"" />()</c> <br/>
        /// For value types, equivalent to <c><see langword=""default"" />(<typeparamref name=""T"" />)</c>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static T NewOrDefault<
#if NET5_0_OR_GREATER
{options.DynamicallyAccessedMembers(0)}
#endif
T>()
	    {{
#if NETFRAMEWORK
		    return {options.GlobalNSDot()}{FastNewCoreGenerator.ClassName}<T>.{FastNewCoreGenerator.IsValueTypeTName}
#else
		    return typeof(T).IsValueType
#endif
                ? default(T)! // This will never be null since T is a ValueType
                : FastNew<T>.CompiledDelegate();
	    }}");



        for (int parameterIndex = 1; parameterIndex <= options.MaxParameterCount; parameterIndex++)
        {
            builder.Indent(2);
            builder.Append("public static T CreateInstance");
            builder.DeclareGenericMember(parameterIndex);
            #region MyRegion
            builder.Append('(');
            for (int i = 0; i < parameterIndex; i++)
            {
                if (i != 0)
                    builder.Append(',', ' ');
                builder.AppendGenericArgumentName(i);
                builder.Append(' ');
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.AppendLine(") =>");
            builder.Indent(3);
            builder.GlobalNamespaceDot();
            builder.Append(FastNewCoreGenerator.ClassName);
            builder.UseGenericMember(parameterIndex);
            builder.Append('.');
            builder.Append(FastNewCoreGenerator.CompiledDelegateName);
            builder.Append('(');

            for (int i = 0; i < parameterIndex; i++)
            {
                if (i != 0)
                    builder.Append(',', ' ');
                builder.AppendGenericMethodArgumentName(i);
            }

            builder.Append(')', ';');
            builder.AppendLine();
            #endregion
            builder.PrettyNewLine();
        }

        builder.EndBlock(1);
        builder.EndNamespace();

        return builder.BuildAndDispose(this);
    }

    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue)
    {
        return base.ShouldUpdate(oldValue, newValue) && newValue.GenerateCreateInstance;
    }
}
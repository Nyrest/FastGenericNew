﻿namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class ThrowHelperGenerator : CodeGenerator<ThrowHelperGenerator>
{
    public override string Filename => "ThrowHelper.g.cs";

    internal const string ClassName = "FastNewThrowHelper";

    internal const string SmartThrowName = "SmartThrowImpl";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(2048, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.AppendLine(@$"
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal static partial class {ClassName}
    {{
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining | global::System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
#if NET5_0_OR_GREATER
        [global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute(""SmartThrowImpl``1()"", typeof({options.GlobalNSDot()}{ClassName}))]
#endif
");
        builder.AppendLine(@$"
        public static global::System.Reflection.MethodInfo GetSmartThrow<T>() => typeof({options.GlobalNSDot()}{ClassName}).GetMethod(""SmartThrowImpl"", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)!.MakeGenericMethod(typeof(T));

        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining | global::System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
        public static T SmartThrowImpl<T>()
        {{
            var qualifiedName = typeof(T).AssemblyQualifiedName;

            if (typeof(T).IsInterface)
                throw new global::System.MissingMethodException($""Cannot create an instance of an interface: '{{ qualifiedName }}'"");

            if (typeof(T).IsAbstract)
                throw new global::System.MissingMethodException($""Cannot create an abstract class: '{{ qualifiedName }}'"");

            throw new global::System.MissingMethodException($""No match constructor found in type: '{{ qualifiedName }}'"");
        }}
    }}");
        builder.EndNamespace();
        return builder.BuildAndDispose(this);
    }

    public override GeneratorOptions GetOptionsSubset(GeneratorOptions options)
    {
        return base.GetOptionsSubset(options) with
        {
            AlertGeneratedFile = options.AlertGeneratedFile,
        };
    }
}
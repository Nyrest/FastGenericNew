namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class ThrowHelperGenerator : CodeGenerator<ThrowHelperGenerator>
{
    public override string Filename => "ThrowHelper.g.cs";

    internal const string ClassName = "ThrowHelper";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(2048, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.AppendLine(@$"
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static partial class ThrowHelper
    {{
        [MethodImpl(MethodImplOptions.NoInlining)]");
        if (options.Trimmable)
            builder.AppendLine(@$"
#if NET5_0_OR_GREATER
        [DynamicDependency(""SmartThrowImpl``1()"", typeof({options.GlobalNSDot()}{ClassName}))]
#endif");
        builder.AppendLine(@$"
        public static System.Reflection.MethodInfo GetSmartThrow<T>() => typeof({options.GlobalNSDot()}ThrowHelper).GetMethod(""SmartThrowImpl"", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).MakeGenericMethod(typeof(T));

        public static T SmartThrowImpl<T>(ConstructorInfo? constructor)
        {{
            if (typeof(T).IsInterface)
                throw new System.MissingMethodException($""Cannot create an instance of an interface: '{{typeof(T).AssemblyQualifiedName}}'"");

            if (typeof(T).IsAbstract)
                throw new System.MissingMethodException($""Cannot create an abstract class: '{{typeof(T).AssemblyQualifiedName}}'"");

            if (constructor == null && !typeof(T).IsValueType)
                throw new System.MissingMethodException($""No match constructor found in type: '{{typeof(T).AssemblyQualifiedName}}'"");

            throw new System.MissingMethodException($""Unknown Error"");
        }}
    }}");
        builder.EndNamespace();
        return builder.BuildAndDispose(this);
    }

    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue) =>
        oldValue.Namespace != newValue.Namespace
        || oldValue.GeneratedAlert != newValue.GeneratedAlert;
}
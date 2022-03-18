namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class DynMetClosureGenerator : CodeGenerator<TypeNewGenerator>
{
    public override string Filename => "_DynMetClosure.g.cs";

    internal const string ClassName = "_FastNewDynMetClosure";

    internal const string InstanceOnlyArrayName = "InstanceOnlyArray";

    internal const string InstanceName = "Instance";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(1536, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.AppendLine(@$"
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed partial class {ClassName}
    {{
        public static readonly Type[] {InstanceOnlyArrayName} = new Type[] {{ typeof({options.GlobalNSDot()}{ClassName}) }};

        public static readonly {options.GlobalNSDot()}{ClassName} {InstanceName} = new {options.GlobalNSDot()}{ClassName}();
    }}");
        builder.EndNamespace();
        return builder.BuildAndDispose(this);
    }
}
using System.Runtime.InteropServices;

namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class GenerationInfoGenerator : CodeGenerator<GenerationInfoGenerator>
{
    public override string Filename => "_GenerationInfo.g.cs";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        if (!options.OutputGenerationInfo) return CodeGenerationResult.Empty;
        CodeBuilder builder = new(2048, in options);

        builder.Append("/*\n");

        builder.AppendLine();
        builder.AppendLine("  Environment:");
        builder.AppendLine();
        builder.AppendLine("    Version = " + Assembly.GetCallingAssembly().GetName().Version.ToString());
        /*
        builder.AppendLine("    Runtime = " + RuntimeInformation.FrameworkDescription);
        builder.AppendLine("    System = " + RuntimeInformation.OSDescription);
        builder.AppendLine("    SystemArch = " + RuntimeInformation.OSArchitecture.ToString());
        builder.AppendLine("    ProcessArch = " + RuntimeInformation.ProcessArchitecture);
        builder.AppendLine("    ProcessorCount = " + Environment.ProcessorCount);
        */
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine("  KeyValues:");
        builder.AppendLine();

        var defaultOptions = new GeneratorOptions(null);

        foreach (var property in typeof(GeneratorOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            builder.Append("    ");
            builder.Append(property.Name);
            builder.Append(" = ");

            var defaultValue = property.GetValue(defaultOptions);
            var value = property.GetValue(options);

            builder.Append(value.ToString());

            if (!value.Equals(defaultValue))
            {
                builder.Append(' ', ' ');
                builder.Append("(default: ");
                builder.Append(defaultValue.ToString());
                builder.Append(')');
            }

            builder.AppendLine();
        }

        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine("  MSBuild Properties:");
        builder.AppendLine();

        foreach (var property in typeof(GeneratorOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            string name = SourceGeneratorExtensions.prefix + property.Name;
            builder.Append("    ");

            builder.Append('<');
            builder.Append(name);
            builder.Append('>');

            builder.Append(property.GetValue(options).ToString());

            builder.Append('<');
            builder.Append(name);
            builder.Append(" />");

            builder.AppendLine();
        }

        builder.AppendLine();
        builder.Append('*', '/');

        return builder.BuildAndDispose(this);
    }

    // Since this won't be invoked if the oldValue equals the newValue.
    // So just do it.
    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue) => newValue.OutputGenerationInfo;
}

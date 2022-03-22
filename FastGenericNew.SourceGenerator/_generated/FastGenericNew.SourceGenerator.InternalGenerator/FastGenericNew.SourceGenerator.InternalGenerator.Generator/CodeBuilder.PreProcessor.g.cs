#nullable enable

namespace FastGenericNew.SourceGenerator.Utilities;

partial struct CodeBuilder
{
	private partial void _Write_PreProcessorDefinitions()
    {
        if (Options.AllowUnsafeImplementation)
		    AppendLine($"#define {Const_PreProcessDefinePrefix}{nameof(Options.AllowUnsafeImplementation)}");
    }
}
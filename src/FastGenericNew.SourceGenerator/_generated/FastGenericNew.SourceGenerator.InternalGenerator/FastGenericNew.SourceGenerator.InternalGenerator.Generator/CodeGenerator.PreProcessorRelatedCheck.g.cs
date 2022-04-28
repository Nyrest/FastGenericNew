#nullable enable

namespace FastGenericNew.SourceGenerator;

partial class CodeGenerator
{
    private static partial bool PreProcessorRelatedCheck(in GeneratorOptions oldValue, in GeneratorOptions newValue) =>
        oldValue.AllowUnsafeImplementation != newValue.AllowUnsafeImplementation
;
}
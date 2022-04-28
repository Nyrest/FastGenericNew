using System.Collections.Immutable;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FastGenericNew.SourceGenerator.InternalGenerator.Gen
{
    public static class GeneratorOptionsRelatedGenerator
    {
        public static void Register(in IncrementalGeneratorInitializationContext context)
        {
            var declarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                static (syntax, _) => syntax is PropertyDeclarationSyntax { AttributeLists.Count: > 0 },
                static (syntaxContext, cancellationToken) =>
                {
                    var semanticModel = syntaxContext.SemanticModel;
                    var syntax = (PropertyDeclarationSyntax)syntaxContext.Node;
                    foreach (AttributeListSyntax attributeListSyntax in syntax.AttributeLists)
                    {
                        foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                        {
                            if (semanticModel.GetSymbolInfo(attributeSyntax, cancellationToken).Symbol is IMethodSymbol attributeSymbol &&
                                attributeSymbol.ContainingType.ToDisplayString() == GeneratorOptionAttributeGenerator.AttributeFullName)
                            {
                                return syntax;
                            }
                        }
                    }
                    return null!; // Disable nullable warning
                    // 👇 Null items will be removed next line.
                })
            .Where(static c => c is not null);

            var compilationAndDeclarations =
                context.CompilationProvider.Combine(declarations.Collect());

            context.RegisterSourceOutput(compilationAndDeclarations, (spc, source) => BuildSource(in spc, source.Left, source.Right));
        }

        private static void BuildSource(in SourceProductionContext context, Compilation compilation, ImmutableArray<PropertyDeclarationSyntax> syntaxs)
        {
            var symbolsWithAttrData = syntaxs
                .Select(propertySyntax =>
                {
                    var symbol = (IPropertySymbol)compilation.GetSemanticModel(propertySyntax.SyntaxTree).GetDeclaredSymbol(propertySyntax)!;
                    var attrData = symbol.GetAttributes().First(x => x.AttributeClass!.ToDisplayString() == GeneratorOptionAttributeGenerator.AttributeFullName);
                    return
                    (
                        symbol,
                        attrData
                    );
                })
                .ToArray();
            BuildGeneratorOptionsConstructor(in context, symbolsWithAttrData);
            BuildCodeBuilderPreProcessorDefinitions(in context, symbolsWithAttrData);
            BuildCodeGeneratorExtraCheck(in context, symbolsWithAttrData);
        }

        public static void BuildGeneratorOptionsConstructor(in SourceProductionContext context, (IPropertySymbol symbol, AttributeData attrData)[] symbolsWithAttrData)
        {
            const string indent = "        ";
            StringBuilder sb = new StringBuilder("""
#nullable enable

namespace FastGenericNew.SourceGenerator;

partial record struct GeneratorOptions
{
    public GeneratorOptions(AnalyzerConfigOptionsProvider? provider)
    {
        var options = provider?.GlobalOptions;

""", 4096);

            foreach (var (symbol, attrData) in symbolsWithAttrData)
            {
                var propertyName = symbol.Name;
                var propertyType = symbol.Type;
                var defaultValueExpression = attrData.ConstructorArguments[0].ToCSharpString();

                sb.Append(indent);
                sb.Append(propertyName);
                sb.AppendLine($" = options.GetOrDefault(nameof({propertyName}), {defaultValueExpression});");
            }

            sb.Append("""
    }
}
""");
            context.AddSource("GeneratorOptions.ctor.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }

        public static void BuildCodeBuilderPreProcessorDefinitions(in SourceProductionContext context, (IPropertySymbol symbol, AttributeData attrData)[] symbolsWithAttrData)
        {
            StringBuilder sb = new StringBuilder("""
#nullable enable

namespace FastGenericNew.SourceGenerator.Utilities;

partial struct CodeBuilder
{
	private partial void _Write_PreProcessorDefinitions()
    {

""", 4096);

            foreach (var (symbol, attrData) in symbolsWithAttrData)
            {
                if (!attrData.TryGetNamedArgument(GeneratorOptionAttributeGenerator.Arg_PresentPreProcessor, out bool value) || !value)
                    continue;
                var propertyName = symbol.Name;

                sb.AppendLine($$"""
        if (Options.{{propertyName}})
		    AppendLine($"#define {Const_PreProcessDefinePrefix}{nameof(Options.{{propertyName}})}");
""");
            }

            sb.Append("""
    }
}
""");
            context.AddSource("CodeBuilder.PreProcessor.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }

        public static void BuildCodeGeneratorExtraCheck(in SourceProductionContext context, (IPropertySymbol symbol, AttributeData attrData)[] symbolsWithAttrData)
        {
            const string indent = "        ";
            StringBuilder sb = new StringBuilder("""
#nullable enable

namespace FastGenericNew.SourceGenerator;

partial class CodeGenerator
{
    private static partial bool PreProcessorRelatedCheck(in GeneratorOptions oldValue, in GeneratorOptions newValue) =>

""", 4096);
            bool isFirst = true;
            foreach (var (symbol, attrData) in symbolsWithAttrData)
            {
                if (!attrData.TryGetNamedArgument(GeneratorOptionAttributeGenerator.Arg_PresentPreProcessor, out bool value) || !value)
                    continue;
                var propertyName = symbol.Name;
                sb.Append(indent);

                if (!isFirst)
                {
                    sb.Append("|| ");
                }
                else isFirst = false;
                sb.AppendLine($"oldValue.{propertyName} != newValue.{propertyName}");
            }
            if (isFirst) sb.Append("false");
            sb.Append("""
;
}
""");
            context.AddSource("CodeGenerator.PreProcessorRelatedCheck.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }
    }
}

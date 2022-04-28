
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;

using FastGenericNew.SourceGenerator.InternalGenerator.Gen;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FastGenericNew.SourceGenerator.InternalGenerator;

[Generator(LanguageNames.CSharp)]
public unsafe class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        GeneratorOptionAttributeGenerator.Register(in context);
        GeneratorOptionsRelatedGenerator.Register(in context);
        RoslynPropsFileGenerator.Register(in context);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace FastGenericNew.SourceGenerator.InternalGenerator.Gen
{
    public static class GeneratorOptionAttributeGenerator
    {
        private const string Namespace = "FastGenericNew.SourceGenerator";

        private const string ClassName = "GeneratorOptionAttribute";

        public const string AttributeFullName = $"{Namespace}.{ClassName}";

        public const string Arg_PresentPreProcessor = "PresentPreProcessor";

        public static void Register(in IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(static spc =>
            {
                spc.AddSource("GeneratorOptionAttribute.cs", SourceText.From($$"""
namespace {{Namespace}}
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class {{ClassName}} : Attribute
    {
        public bool PresentPreProcessor { get; set; }

        public {{ClassName}}(object defaultValue) { }
    }
}
""", Encoding.UTF8));
            });
        }
    }
}

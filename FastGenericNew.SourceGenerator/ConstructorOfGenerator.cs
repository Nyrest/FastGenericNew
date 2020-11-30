using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using static FastGenericNew.SourceGenerator.CompileSettings;

namespace FastGenericNew.SourceGenerator
{
    [Generator]
    public class ConstructorOfGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder sourceBuilder = new StringBuilder(@"
using System;
using System.Reflection;

namespace FastGenericNew {
");
            StringBuilder tparametersBuilder = new StringBuilder(13 * maxParameterCount);
            for (int index = 0; index < maxParameterCount + 1; index++)
            {
                if (index == 0)
                {
                    sourceBuilder.Append(@"
public static partial class ConstructorOf<T>
{
    public static readonly ConstructorInfo value =
        typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
}
");
                }
                else
                {
                    tparametersBuilder.Append(",TParameter" + (index - 1).ToString());

                    string tparameters = tparametersBuilder.ToString();

                    sourceBuilder.Append(publicConstructorOf 
                        ? "public static class ConstructorOf<T"
                        : "internal static class ConstructorOf<T");
                    sourceBuilder.Append(tparameters);
                    sourceBuilder.Append(">\n{");

                    sourceBuilder.Append(@"public static readonly ConstructorInfo value =
typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] {");
                    for (int i = 0; i < index; i++)
                    {
                        sourceBuilder.Append("typeof(TParameter" + i + "),");
                    }
                    sourceBuilder.Append("},null);}");
                }
            }
            sourceBuilder.Append('}');
            context.AddSource("constructorOfGenerated", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required
        }
    }
}

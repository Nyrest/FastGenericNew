using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using static FastGenericNew.SourceGenerator.CompileSettings;
// TODO: Optimize this messed trash

namespace FastGenericNew.SourceGenerator
{
    [Generator]
    public class FastNewGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            int i;
            StringBuilder sourceBuilder = new StringBuilder(@"
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
namespace FastGenericNew {
");
            StringBuilder tparametersBuilder = new StringBuilder(13 * maxParameterCount);
            StringBuilder parametersVarsBuilder = new StringBuilder(3 * maxParameterCount);
            for (int index = 0; index < maxParameterCount + 1; index++)
            {
                
                if (index == 0)
                {
                    sourceBuilder.Append(@"
public static partial class FastNew<T>
{
    public static readonly Expression<Func<T>> SourceExpression =
        !typeof(T).IsValueType
        ? Expression.Lambda<Func<T>>(Expression.New(ConstructorOf<T>.value))
        : Expression.Lambda<Func<T>>(Expression.New(typeof(T)));

    public static readonly Func<T> CreateInstance = SourceExpression.Compile();
}
");
                }
                else
                {
                    tparametersBuilder.Append(",TParameter" + (index - 1).ToString());
                    parametersVarsBuilder.Append(",p" + (index - 1).ToString());

                    string tparameters = tparametersBuilder.ToString();
                    string parametersVars = parametersVarsBuilder.ToString().Substring(1);

                    sourceBuilder.Append(@"public static partial class FastNew<T");
                    sourceBuilder.Append(tparameters);
                    sourceBuilder.Append(">\n{");

                    string delegateType;
                    if (index <= 16)
                    {
                        delegateType = "Func<" + tparameters.Substring(1) + ",T>";
                    }
                    else
                    {
                        sourceBuilder.Append($"public delegate T FastNewDelegate(");
                        for (i = 0; i < index; i++)
                        {
                            if (i != 0) sourceBuilder.Append(',');
                            sourceBuilder.Append("TParameter" + i + " arg" + i);
                        }
                        sourceBuilder.Append(");");
                        delegateType = "FastNewDelegate";
                    }

                    sourceBuilder.Append($"public static readonly Expression<{delegateType}> SourceExpression;");

                    sourceBuilder.Append("public static readonly " + delegateType + " CreateInstance;\n");

                    sourceBuilder.Append("static FastNew() {\n");
                    for (i = 0; i < index; i++)
                    {
                        sourceBuilder.Append($"ParameterExpression p{i} = Expression.Parameter(typeof(TParameter{i}));");
                    }
                    sourceBuilder.Append($@"
CreateInstance = (SourceExpression = Expression.Lambda<{delegateType}>(Expression.New(
    ConstructorOf<T{tparameters}>.value,
    {parametersVars}),
    {parametersVars})).Compile();
");
                    sourceBuilder.Append("}}"); // Constructor End then Class End
                }
            }
            sourceBuilder.Append('}'); // Namespace End
            context.AddSource("fastNewGenerated", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required
        }
    }
}

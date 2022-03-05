using System.Linq.Expressions;

namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class FastNewCoreGenerator : CodeGenerator<FastNewCoreGenerator>
{
    public override string Filename => "FastNew{T}.g.cs";

    internal const string ClassName = "FastNew";

    internal const string IsValidName = "IsValid";

    internal const string CompiledDelegateName = "CompiledDelegate";

    internal const string IsValueTypeTName = "_isValueTypeT";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(65536, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.Indent(1);
        builder.AppendAccessibility(options.PublicFastNewCore);
        builder.AppendLine(@$"static partial class FastNew<
#if NET5_0_OR_GREATER
{options.DynamicallyAccessedMembers(0)}
#endif
T>
    {{
#if NETFRAMEWORK
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly bool _isValueTypeT = typeof(T).IsValueType;
#endif

        {(options.PublicSourceExpression ? "public" : "internal")} static readonly System.Linq.Expressions.Expression<Func<T>> SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<T>>(typeof(T).IsValueType
            ? ({options.GlobalNSDot()}{ConstructorCacheGenerator.ClassName}<T>.{ConstructorCacheGenerator.ValueName} != null
                ? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New({options.GlobalNSDot()}{ConstructorCacheGenerator.ClassName}<T>.{ConstructorCacheGenerator.ValueName})
                : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(typeof(T)))
            : (({options.GlobalNSDot()}{ConstructorCacheGenerator.ClassName}<T>.{ConstructorCacheGenerator.ValueName} != null && !typeof(T).IsAbstract)
                ? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New({options.GlobalNSDot()}{ConstructorCacheGenerator.ClassName}<T>.{ConstructorCacheGenerator.ValueName})
                : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call({options.GlobalNSDot()}{ThrowHelperGenerator.ClassName}.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant({options.GlobalNSDot()}{ConstructorCacheGenerator.ClassName}<T>.{ConstructorCacheGenerator.ValueName}, typeof(ConstructorInfo))))
            , Array.Empty<System.Linq.Expressions.ParameterExpression>());

	    {(options.PublicCompiledDelegate ? "public" : "internal")} static readonly Func<T> {CompiledDelegateName} = SourceExpression.Compile();
    
        public static readonly bool {IsValidName} = typeof(T).IsValueType || ({options.GlobalNSDot()}{ConstructorCacheGenerator.ClassName}<T>.{ConstructorCacheGenerator.ValueName} != null && !typeof(T).IsAbstract);
    }}");

        for (int parameterIndex = 1; parameterIndex <= options.MaxParameterCount; parameterIndex++)
        {
            builder.Indent(1);
            builder.AppendAccessibility(options.PublicFastNewCore);
            builder.AppendKeyword("static partial class");

            builder.Append(ClassName);
            builder.DeclareGenericMember(parameterIndex);
            builder.AppendLine();

            builder.StartBlock(1);

            builder.Indent(2);
            builder.AppendAccessibility(options.PublicSourceExpression);
            builder.Append("static readonly System.Linq.Expressions.Expression<");
            builder.UseGenericDelegate(parameterIndex);
            builder.AppendLine("> SourceExpression;");
            builder.PrettyNewLine();

            builder.Indent(2);
            builder.AppendAccessibility(options.PublicCompiledDelegate);
            builder.Append("static readonly ");
            builder.UseGenericDelegate(parameterIndex);
            builder.AppendLine($" {CompiledDelegateName};");
            builder.PrettyNewLine();

            builder.Indent(2);
            builder.AppendLine($"public static readonly bool {IsValidName};");
            builder.PrettyNewLine();

            #region Constructor
            builder.Indent(2);
            builder.AppendLine("static FastNew()");
            builder.StartBlock(2);

            #region Var constructor
            builder.Indent(3);
            builder.Append("var constructor = ");
            builder.GlobalNamespaceDot();
            builder.Append(ConstructorCacheGenerator.ClassName);
            builder.UseGenericMember(parameterIndex);
            builder.AppendLine($".{ConstructorCacheGenerator.ValueName};");
            #endregion

            #region IsValid
            builder.Indent(3);
            builder.AppendLine("IsValid = constructor != null && !typeof(T).IsAbstract;");
            #endregion

            #region Parameters
            for (int i = 0; i < parameterIndex; i++)
            {
                builder.Indent(3);
                builder.Append("var ");
                builder.AppendGenericMethodArgumentName(i);
                builder.Append(" = System.Linq.Expressions.Expression.Parameter(typeof(");
                builder.AppendGenericArgumentName(i);
                builder.Append("));\n");
            }
            #endregion

            #region Final
            builder.Indent(3);
            builder.Append($"{CompiledDelegateName} = (SourceExpression = System.Linq.Expressions.Expression.Lambda<");
            builder.UseGenericDelegate(parameterIndex);
            builder.AppendLine($">({IsValidName}");

            builder.Indent(4);
            builder.Append("? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!");
            /*
            builder.GlobalNamespaceDot();
            builder.Append(ConstructorOfGenerator.ClassName);
            builder.UseGenericMember(parameterIndex);
            builder.Append($".{ConstructorOfGenerator.ValueName}");
            */
            for (int i = 0; i < parameterIndex; i++)
            {
                builder.Append(',', ' ');
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.AppendLine(')');

            builder.Indent(4);
            builder.Append(": (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(");
            builder.GlobalNamespaceDot();
            builder.Append($"{ThrowHelperGenerator.ClassName}.GetSmartThrow<T>(), ");
            builder.Append($"System.Linq.Expressions.Expression.Constant({options.GlobalNSDot()}{ConstructorCacheGenerator.ClassName}<T>.{ConstructorCacheGenerator.ValueName}, typeof(ConstructorInfo))");
            builder.AppendLine(')');

            builder.Indent(3);
            builder.Append(", new System.Linq.Expressions.ParameterExpression[] { ");
            for (int i = 0; i < parameterIndex; i++)
            {
                if (i != 0)
                {
                    builder.Append(',', ' ');
                }
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.AppendLine(" })).Compile();");

            #endregion

            builder.EndBlock(2);
            #endregion

            builder.EndBlock(1);
        }

        builder.EndNamespace();

        return builder.BuildAndDispose(this);
    }

    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue) =>
        base.ShouldUpdate(oldValue, newValue)
        || oldValue.ForceFastNewDelegate != newValue.ForceFastNewDelegate
        || oldValue.PublicSourceExpression != newValue.PublicSourceExpression
        || oldValue.PublicCompiledDelegate != newValue.PublicCompiledDelegate;
}
namespace FastGenericNew.SourceGenerator.CodeGenerators;

public class FastNewCoreGenerator : CodeGenerator<FastNewCoreGenerator>
{
    public override string Filename => "FastNew{T}.g.cs";

    internal const string ClassName = "FastNew";

    internal const string IsValidName = "IsValid";

    internal const string CompiledDelegateName = "CompiledDelegate";

    internal const string IsValueTypeTName = "_isValueTypeT";

    internal const string ConsructorName = "CachedConstructor";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(65536, in options);
        builder.WriteFileHeader();
        builder.StartNamespace();
        builder.Indent(1);
        builder.AppendAccessibility(options.PublicFastNewCore);

        // Use Expression on .NET Framework
        // Use ILGenerator on .NET Core
        const string UseExpressionCondition = "NETFRAMEWORK";

        #region Get CompiledDelegateName Type
        string compiledDelegateTypeNoParam;
        {
            CodeBuilder internalBuilder = new(32, in options);
            internalBuilder.UseGenericDelegate(0);
            compiledDelegateTypeNoParam = internalBuilder.ToString();
        }
        #endregion
        builder.Pre_If(UseExpressionCondition);
        #region Expression
        builder.AppendLine(@$"static partial class {ClassName}<
#if NET5_0_OR_GREATER
{options.DynamicallyAccessedMembers(0)}
#endif
T>
    {{
#if NETFRAMEWORK
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        internal static readonly bool _isValueTypeT = typeof(T).IsValueType;
#endif
		/// <summary>
		/// The constructor of <typeparamref name=""T"" /> with given arguments. <br/>
		/// Could be <see langword=""null"" /> if the constructor couldn't be found.
		/// </summary>
		public static readonly global::System.Reflection.ConstructorInfo? {ConsructorName} = typeof(T).GetConstructor({(options.NonPublicConstructorSupport
? "global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic"
: "global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public")}, null, global::System.Type.EmptyTypes, null);

	    {(options.PublicCompiledDelegate ? "public" : "internal")} static readonly global::System.Func<T> {CompiledDelegateName} = global::System.Linq.Expressions.Expression.Lambda<Func<T>>(typeof(T).IsValueType
            ? ({options.GlobalNSDot()}{ClassName}<T>.{ConsructorName} != null
                ? (global::System.Linq.Expressions.Expression)global::System.Linq.Expressions.Expression.New({options.GlobalNSDot()}{ClassName}<T>.{ConsructorName})
                : (global::System.Linq.Expressions.Expression)global::System.Linq.Expressions.Expression.New(typeof(T)))
            : (({options.GlobalNSDot()}{ClassName}<T>.{ConsructorName} != null && !typeof(T).IsAbstract)
                ? (global::System.Linq.Expressions.Expression)global::System.Linq.Expressions.Expression.New({options.GlobalNSDot()}{ClassName}<T>.{ConsructorName})
                : (global::System.Linq.Expressions.Expression)global::System.Linq.Expressions.Expression.Call({options.GlobalNSDot()}{ThrowHelperGenerator.ClassName}.GetSmartThrow<T>()))
            , Array.Empty<System.Linq.Expressions.ParameterExpression>()).Compile();
    
        public static readonly bool {IsValidName} = typeof(T).IsValueType || ({options.GlobalNSDot()}{ClassName}<T>.{ConsructorName} != null && !typeof(T).IsAbstract);
    }}");
        #endregion
        builder.Pre_Else();
        #region IL
        builder.AppendLine(@$"static partial class {ClassName}<
#if NET5_0_OR_GREATER
{options.DynamicallyAccessedMembers(0)}
#endif
T>
    {{
		/// <summary>
		/// The constructor of <typeparamref name=""T"" /> with given arguments. <br/>
		/// Could be <see langword=""null"" /> if the constructor couldn't be found.
		/// </summary>
		public static readonly ConstructorInfo? {ConsructorName} = typeof(T).GetConstructor({(options.NonPublicConstructorSupport ? "BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic" : "BindingFlags.Instance | BindingFlags.Public")}, null, Type.EmptyTypes, null);

#if NETFRAMEWORK
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly bool _isValueTypeT = typeof(T).IsValueType;
#endif

	    {(options.PublicCompiledDelegate ? "public" : "internal")} static readonly {compiledDelegateTypeNoParam} {CompiledDelegateName};
    
        public static readonly bool {IsValidName} = typeof(T).IsValueType || ({options.GlobalNSDot()}{ClassName}<T>.{ConsructorName} != null && !typeof(T).IsAbstract);
    
        static {ClassName}()
        {{
            var dm = new DynamicMethod("""", typeof(T), {options.GlobalNSDot()}{DynMetClosureGenerator.ClassName}.{DynMetClosureGenerator.InstanceOnlyArrayName}, restrictedSkipVisibility: true);
            var il = dm.GetILGenerator();
            if ({IsValidName})
            {{
                if ({options.GlobalNSDot()}{ClassName}<T>.{ConsructorName} != null)
                    il.Emit(OpCodes.Newobj, {ConsructorName}!);
                else
                {{
                    il.DeclareLocal(typeof(T));
                    //il.Emit(OpCodes.Ldloca_S, (short)0)
                    //il.Emit(OpCodes.Initobj, typeof(T));
                    il.Emit(OpCodes.Ldloc_0);
                }}
            }}
            else
            {{
                il.Emit(OpCodes.Call, {options.GlobalNSDot()}{ThrowHelperGenerator.ClassName}.GetSmartThrow<T>());
            }}
            il.Emit(OpCodes.Ret);
            {CompiledDelegateName} = ({compiledDelegateTypeNoParam})dm.CreateDelegate(typeof({compiledDelegateTypeNoParam}), {options.GlobalNSDot()}{DynMetClosureGenerator.ClassName}.{DynMetClosureGenerator.InstanceName});
        }}
    }}");
        #endregion
        builder.Pre_EndIf();

        for (int parameterIndex = 1; parameterIndex <= options.MaxParameterCount; parameterIndex++)
        {
            builder.Indent(1);
            builder.AppendAccessibility(options.PublicFastNewCore);
            builder.AppendKeyword("static partial class");

            builder.Append(ClassName);
            builder.DeclareGenericMember(parameterIndex);
            builder.AppendLine();

            builder.StartBlock(1);

            #region CachedConstructor
            builder.XmlDoc(2, @"
/// <summary>
/// The constructor of <typeparamref name=""T"" /> with given arguments. <br/>
/// Could be <see langword=""null"" /> if the constructor couldn't be found.
/// </summary>");

            builder.Indent(2);
            builder.Append($"public static readonly ConstructorInfo? {ConsructorName} = typeof(T).GetConstructor(");
            builder.Append(options.NonPublicConstructorSupport
                ? "BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic"
                : "BindingFlags.Instance | BindingFlags.Public");
            builder.Append(", null, ");
            if (parameterIndex == 0)
            {
                builder.Append("Type.EmptyTypes");
            }
            else
            {
                builder.Append("new Type[]");
                builder.AppendLine();
                builder.StartBlock(2);

                for (int i = 0; i < parameterIndex; i++)
                {
                    builder.Indent(3);
                    builder.Append("typeof(");
                    builder.AppendGenericArgumentName(i);
                    builder.Append(')', ',');
                    builder.AppendLine();
                }

                builder.EndBlock(2, false);
            }
            builder.AppendLine(", null);");
            #endregion

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
            builder.AppendLine($"static {ClassName}()");
            builder.StartBlock(2);

            #region IsValid
            builder.AppendLine(3, $"IsValid = {ConsructorName} != null && !typeof(T).IsAbstract;");
            #endregion

            builder.Pre_If(UseExpressionCondition);
            #region Expression

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

            builder.Indent(3);
            builder.Append($"{CompiledDelegateName} = (System.Linq.Expressions.Expression.Lambda<");
            builder.UseGenericDelegate(parameterIndex);
            builder.AppendLine($">({IsValidName}");

            builder.Indent(4);
            builder.Append($"? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New({ConsructorName}!");
            for (int i = 0; i < parameterIndex; i++)
            {
                builder.Append(',', ' ');
                builder.AppendGenericMethodArgumentName(i);
            }
            builder.AppendLine(')');

            builder.Indent(4);
            builder.Append(": (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(");
            builder.GlobalNamespaceDot();
            builder.AppendLine($"{ThrowHelperGenerator.ClassName}.GetSmartThrow<T>())");

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
            builder.Pre_Else();
            #region IL
            #region Final
            builder.Append(3, $@"var dm = new DynamicMethod("""", typeof(T), new Type[] {{ typeof({options.GlobalNSDot()}{DynMetClosureGenerator.ClassName}), ");
            for (int i = 0; i < parameterIndex; i++)
            {
                if (i != 0)
                {
                    builder.Append(',', ' ');
                }
                builder.Append("typeof(");
                builder.AppendGenericArgumentName(i);
                builder.Append(')');
            }
            builder.Append(' ', '}');
            builder.AppendLine(@", restrictedSkipVisibility: true);");
            // 5 = Newobj + Ret
            // +4 is required because EnsureCapacity() in il.Emit needs at least 3 bytes more available in buffer.
            // TODO: more optimization is needed here.
            builder.AppendLine(3, $"var il = dm.GetILGenerator({5 + parameterIndex + 4});");

            builder.AppendLine(3, $"if ({IsValidName})");
            builder.AppendLine(3, $"{{");
            #region Parameters
            for (int i = 0; i < parameterIndex; i++)
            {
                var actual = i + 1;
                builder.Append(4, "il.Emit(");
                builder.Append(actual switch
                {
                    0 => "OpCodes.Ldarg_0",
                    1 => "OpCodes.Ldarg_1",
                    2 => "OpCodes.Ldarg_2",
                    3 => "OpCodes.Ldarg_3",
                    _ when actual <= 255 => $"OpCodes.Ldarg_S, (byte){actual}",
                    _ => $"OpCodes.Ldarg, (short){actual}",
                });
                builder.Append(')', ';');
                builder.AppendLine();
            }
            #endregion
            builder.AppendLine(4, $"il.Emit(OpCodes.Newobj, {ConsructorName}!);");

            builder.AppendLine(3, $"}}");
            builder.AppendLine(3, $"else");
            builder.AppendLine(3, $"{{");
            builder.AppendLine(4, $"il.Emit(OpCodes.Call, {options.GlobalNSDot()}{ThrowHelperGenerator.ClassName}.GetSmartThrow<T>());");
            builder.AppendLine(3, $"}}");

            builder.AppendLine(3, "il.Emit(OpCodes.Ret);");

            builder.Append(3, CompiledDelegateName);
            builder.Append(" = (");
            builder.UseGenericDelegate(parameterIndex);
            builder.Append(")dm.CreateDelegate(typeof(");
            builder.UseGenericDelegate(parameterIndex);
            builder.AppendLine($"), {options.GlobalNSDot()}{DynMetClosureGenerator.ClassName}.{DynMetClosureGenerator.InstanceName});");
            #endregion
            #endregion
            builder.Pre_EndIf();

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
        || oldValue.PublicCompiledDelegate != newValue.PublicCompiledDelegate;
}
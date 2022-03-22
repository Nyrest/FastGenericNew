namespace FastGenericNew.SourceGenerator.CodeGenerators;

internal class ClrAllocatorGenerator : CodeGenerator<ClrAllocatorGenerator>
{
    public override string Filename => "ClrAllocator.g.cs";

    internal const string ClassName = "ClrAllocator";

    internal const string ppEnabled = $"NET6_0_OR_GREATER && {CodeBuilder.Const_PreProcessDefinePrefix}{nameof(GeneratorOptions.AllowUnsafeImplementation)}";

    public override CodeGenerationResult Generate(in GeneratorOptions options)
    {
        CodeBuilder builder = new(6144, in options);
        builder.WriteFileHeader();

        builder.Pre_If(ppEnabled);

        builder.StartNamespace();

        builder.AppendLine($$"""
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    static unsafe class {{ClassName}}
    {
        public static readonly delegate*<void*, ref delegate*<void*, object>, ref void*, ref delegate*<object, void>, int*, void> GetActivationInfo;

        public static readonly bool IsSupported;

        static ClrAllocator()
        {
            foreach (var met in typeof(global::System.RuntimeTypeHandle).GetMethods(global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic))
            {
                if (met.Name == "GetActivationInfo" && (met.Attributes & global::System.Reflection.MethodAttributes.PinvokeImpl) != 0)
                {
                    var parameters = met.GetParameters();
                    // TODO Consider to use list pattern when available
                    // Double-check the method
                    if (
                        parameters.Length == 5
                        && parameters[0].ParameterType == Type.GetType("System.Runtime.CompilerServices.ObjectHandleOnStack", false)
                        && parameters[1].ParameterType == typeof(delegate*<void*, object>*)
                        && parameters[2].ParameterType == typeof(void**)
                        && parameters[3].ParameterType == typeof(delegate*<object, void>*)
                        // && parameters[4].ParameterType == Type.GetType("Interop.BOOL", false)
                        )
                    {
                        GetActivationInfo = (delegate*<void*, ref delegate*<void*, object>, ref void*, ref delegate*<object, void>, int*, void>)
                            met.MethodHandle.GetFunctionPointer();
                        IsSupported = true;
                    }
                }
            }
        }

		public static void CtorNoopStub(object _) { }
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining | global::System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
		public static object ThrowNotSupported(void* _) => throw new global::System.NotSupportedException();
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining | global::System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
        public static object SmartThrow<T>(void* _) => (object){{options.GlobalNSDot()}}{{ThrowHelperGenerator.ClassName}}.{{ThrowHelperGenerator.SmartThrowName}}<T>()!;
    }

    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    static unsafe class ClrAllocator<T>
    {
        private static readonly delegate*<void*, object> _pfnAllocator;

        private static readonly void* _allocatorFirstArg;

        private static readonly delegate*<object, void> _pfnCtor;

        /// <summary>
        /// TRUE means ClrAllocator&lt;T&gt; can fully replace to FastNewCore<br/>
        /// So this can be TRUE even the CreateInstance will throw an exception.
        /// </summary>
        public static readonly bool IsSupported;

        static ClrAllocator()
        {
            if (!{{options.GlobalNSDot()}}{{ClassName}}.IsSupported) goto MarkUnsupported;
            var type = typeof(T);

            int _ctorIsPublic = default;
            try
            {
                ((delegate*<void*, ref delegate*<void*, object>, ref void*, ref delegate*<object, void>, int*, void>){{options.GlobalNSDot()}}{{ClassName}}.GetActivationInfo)
                    (Unsafe.AsPointer(ref type), ref _pfnAllocator, ref _allocatorFirstArg, ref _pfnCtor, &_ctorIsPublic);
            }
            catch
            {
                // Exceptions SmartThrow can handle.
                //if (type.IsAbstract) goto GoSmartThrow;

                // GetActivationInfo has many extra limits
                // https://github.com/dotnet/runtime/blob/a5ec8aa173e4bc76b173a70aa7fa3be1867011eb/src/coreclr/vm/reflectioninvocation.cpp#L1942:25
  
                // Exceptions SmartThrow CAN NOT handle.
                // Mark unsupported so FastGenericNew will use FastNewCore instead if hit any
                // 
                if (
                    type.IsArray // typeHandle.IsArray()
                    || type.IsByRefLike // pMT->IsByRefLike()
                    || type == typeof(string) // pMT->HasComponentSize()
                    || typeof(Delegate).IsAssignableFrom(type) // pMT->IsDelegate()
                    ) 
                    goto MarkUnsupported;
            }

            if (_pfnAllocator is null)
                goto GoSmartThrow;

            if (_pfnCtor is null)
            {
                if(type.IsValueType)
                    _pfnCtor = &{{options.GlobalNSDot()}}{{ClassName}}.CtorNoopStub;
                else
                    goto GoSmartThrow;
            }

            IsSupported = true;
            return;

GoSmartThrow:
            _pfnAllocator = &{{options.GlobalNSDot()}}{{ClassName}}.SmartThrow<T>;
            IsSupported = true; // read the comment of IsSupported
            return;

MarkUnsupported:
            _pfnAllocator = &{{options.GlobalNSDot()}}{{ClassName}}.ThrowNotSupported;
            IsSupported = false;
            return;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CreateInstance()
        {
            var result = _pfnAllocator(_allocatorFirstArg);
            _pfnCtor(result);
            return (T)result;
        }
    }
""");

        builder.EndNamespace();
        builder.Pre_EndIf();
        return builder.BuildAndDispose(this);
    }

    public override bool ShouldUpdate(in GeneratorOptions oldValue, in GeneratorOptions newValue)
    {
        return base.ShouldUpdate(oldValue, newValue) && newValue.AllowUnsafeImplementation;
    }
}

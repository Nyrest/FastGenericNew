using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace FastGenericNew.Benchmarks.Benchmarks;

public class CtorExprILBenchmark
{
    [Benchmark]
    public void After()
    {
        FastNewIL<DemoClass>.cctor();
    }

    [Benchmark(Baseline = true)]
    public void Before()
    {
        FastNewExpr<DemoClass>.cctor();
    }

    #region Expression
    internal static partial class FastNewExpr<T>
    {
        public static ConstructorInfo? CachedConstructor;

        public static Expression<Func<T>>? SourceExpression;

        public static Func<T>? CompiledDelegate;

        public static bool IsValid;

        public static void cctor()
        {
            CachedConstructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            IsValid = typeof(T).IsValueType || (CachedConstructor != null && !typeof(T).IsAbstract);
            SourceExpression = Expression.Lambda<Func<T>>((!typeof(T).IsValueType) ? ((CachedConstructor != null && !typeof(T).IsAbstract) ? ((Expression)Expression.New(CachedConstructor)) : ((Expression)Expression.Call(ThrowHelper.GetSmartThrow<T>(), Expression.Constant(CachedConstructor, typeof(ConstructorInfo))))) : ((CachedConstructor != null) ? Expression.New(CachedConstructor) : Expression.New(typeof(T))), Array.Empty<ParameterExpression>());
            CompiledDelegate = SourceExpression.Compile();
        }
    }

    #endregion

    #region ILEmit
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed partial class _FastNewDynMetClosure
    {
        public static readonly Type[] InstanceOnlyArray = new Type[] { typeof(_FastNewDynMetClosure) };

        public static readonly _FastNewDynMetClosure Instance = new _FastNewDynMetClosure();
    }


    internal
#if NETFRAMEWORK
static partial class FastNewIL<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
    T>
    {
#if NETFRAMEWORK
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly bool _isValueTypeT = typeof(T).IsValueType;
#endif
        /// <summary>
        /// The constructor of <typeparamref name="T" /> with given arguments. <br/>
        /// Could be <see langword="null" /> if the constructor couldn't be found.
        /// </summary>
        public static ConstructorInfo? CachedConstructor;

        public static Func<T>? CompiledDelegate;

        public static bool IsValid;

        public static void cctor()
        {
            CachedConstructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            IsValid = typeof(T).IsValueType || (FastNewIL<T>.CachedConstructor != null && !typeof(T).IsAbstract);
            CompiledDelegate = System.Linq.Expressions.Expression.Lambda<Func<T>>(typeof(T).IsValueType
            ? (FastNewIL<T>.CachedConstructor != null
                ? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(FastNewIL<T>.CachedConstructor)
                : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(typeof(T)))
            : ((FastNewIL<T>.CachedConstructor != null && !typeof(T).IsAbstract)
                ? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(FastNewIL<T>.CachedConstructor)
                : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(ThrowHelper.GetSmartThrow<T>()))
            , Array.Empty<System.Linq.Expressions.ParameterExpression>()).Compile();
        }
    }
#else
static partial class FastNewIL<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
    T>
    {
        /// <summary>
        /// The constructor of <typeparamref name="T" /> with given arguments. <br/>
        /// Could be <see langword="null" /> if the constructor couldn't be found.
        /// </summary>
        public static ConstructorInfo? CachedConstructor;

#if NETFRAMEWORK
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly bool _isValueTypeT = typeof(T).IsValueType;
#endif

        public static Func<T>? CompiledDelegate;

        public static bool IsValid;

        public static void cctor()
        {
            CachedConstructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            IsValid = typeof(T).IsValueType || (FastNewIL<T>.CachedConstructor != null && !typeof(T).IsAbstract);

            var dm = new DynamicMethod("", typeof(T), _FastNewDynMetClosure.InstanceOnlyArray, restrictedSkipVisibility: true);
            var il = dm.GetILGenerator();
            if (IsValid)
            {
                if (FastNewIL<T>.CachedConstructor != null)
                    il.Emit(OpCodes.Newobj, CachedConstructor!);
                else
                {
                    il.DeclareLocal(typeof(T));
                    //il.Emit(OpCodes.Ldloca_S, (short)0)
                    //il.Emit(OpCodes.Initobj, typeof(T));
                    il.Emit(OpCodes.Ldloc_0);
                }
            }
            else
            {
                il.Emit(OpCodes.Call, ThrowHelper.GetSmartThrow<T>());
            }
            il.Emit(OpCodes.Ret);
            CompiledDelegate = (Func<T>)dm.CreateDelegate(typeof(Func<T>), _FastNewDynMetClosure.Instance);
        }
    }
#endif
    #endregion

    public class DemoClass { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static partial class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]

        public static MethodInfo GetSmartThrow<T>() => typeof(ThrowHelper).GetMethod("SmartThrowImpl", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)!.MakeGenericMethod(typeof(T));

        public static T SmartThrowImpl<T>()
        {
            if (typeof(T).IsInterface)
                throw new MissingMethodException($"Cannot create an instance of an interface: '{typeof(T).AssemblyQualifiedName}'");

            if (typeof(T).IsAbstract)
                throw new MissingMethodException($"Cannot create an abstract class: '{typeof(T).AssemblyQualifiedName}'");

            throw new MissingMethodException($"No match constructor found in type: '{typeof(T).AssemblyQualifiedName}'");
        }
    }
}

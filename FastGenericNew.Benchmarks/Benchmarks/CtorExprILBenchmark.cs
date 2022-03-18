using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace FastGenericNew.Benchmarks.Benchmarks;

public class CtorExprILBenchmark
{
    [Benchmark]
    public void CCtorIL()
    {
        FastNewIL<DemoClass>.cctor();
    }

    [Benchmark(Baseline = true)]
    public void CCtorExpr()
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
    [DebuggerStepThrough]
    internal sealed class Closure
    {
        public static readonly Type[] InstanceOnlyArray = new[] { typeof(Closure) };

        public static readonly Closure Instance = new();
    }

    internal static partial class FastNewIL<T>
    {
        public static ConstructorInfo? CachedConstructor;

        public static Func<T>? CompiledDelegate;

        public static bool IsValid;

        public static void cctor()
        {
            CachedConstructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            IsValid = typeof(T).IsValueType || CachedConstructor != null && !typeof(T).IsAbstract;
            var dm = new DynamicMethod("", typeof(T), Closure.InstanceOnlyArray, true);

            var il = dm.GetILGenerator();
            if (IsValid)
            {
                if (CachedConstructor != null)
                    il.Emit(OpCodes.Newobj, CachedConstructor!);
                else
                {
                    il.DeclareLocal(typeof(T));
                    il.Emit(OpCodes.Ldloc_0);
                }
            }
            else
            {
                il.Emit(OpCodes.Call, ThrowHelper.GetSmartThrow<T>());
            }
            il.Emit(OpCodes.Ret);
            CompiledDelegate = (Func<T>)dm.CreateDelegate(typeof(Func<T>), Closure.Instance);
        }
    }
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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using static FastGenericNew.Benchmarks.Benchmarks.CtorExprILBenchmark;

namespace FastGenericNew.Benchmarks.Benchmarks
{
    public class CtorClrAllocatorBenchmark
    {
        [Benchmark]
        public void CtorClrNew()
        {
            ClrAllocator<DemoClass>.cctor();
        }

        [Benchmark]
        public void CtorILNew()
        {
            FastNewIL<DemoClass>.cctor();
        }

        static unsafe class ClrAllocator
        {
            public static delegate*<Type, out delegate*<void*, object>, out void*, out delegate*<object, void>, out bool, void> GetActivationInfo = (delegate*<Type, out delegate*<void*, object>, out void*, out delegate*<object, void>, out bool, void>)
                typeof(RuntimeTypeHandle).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.Name == "GetActivationInfo" && (x.MethodImplementationFlags & MethodImplAttributes.InternalCall) == 0)
                !.MethodHandle
                .GetFunctionPointer();
        }

        static unsafe class ClrAllocator<T>
        {
            private static delegate*<void*, T> _pfnAllocator;

            private static void* _allocatorFirstArg;

            private static delegate*<T, void> _pfnCtor;

            //private static readonly bool _ctorIsPublic;


            public static void cctor()
            {
                bool _ctorIsPublic;
                ((delegate*<Type, out delegate*<void*, T>, out void*, out delegate*<T, void>, out bool, void>)ClrAllocator.GetActivationInfo)(typeof(T), out _pfnAllocator, out _allocatorFirstArg, out _pfnCtor, out _ctorIsPublic);
            }
        }

        static class FastNewIL<T>
        {
            public static ConstructorInfo? CachedConstructor;

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
    }
}

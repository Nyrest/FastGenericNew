﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static FastGenericNew.Benchmarks.Benchmarks.CtorExprILBenchmark;

namespace FastGenericNew.Benchmarks.Benchmarks
{
    public class ClrAllocatorBenchmark
    {
        [Benchmark]
        public DemoClass ClrNew()
        {
            return ClrAllocator<DemoClass>.CreateInstance();
        }

        [Benchmark]
        public DemoClass ILNew()
        {
            return FastNewIL<DemoClass>.CompiledDelegate();
        }

        [Benchmark]
        public DemoClass Activator()
        {
            return System.Activator.CreateInstance<DemoClass>();
        }

        static unsafe class ClrAllocator
        {
            public static readonly delegate*<ObjectHandleOnStack, ref delegate*<void*, object>, ref void*, ref delegate*<object, void>, int*, void> GetActivationInfo;

            public static readonly bool IsSupported;

            static ClrAllocator()
            {
                foreach (var met in typeof(RuntimeTypeHandle)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic))
                {
                    if (met.Name == "GetActivationInfo" && (met.Attributes & MethodAttributes.PinvokeImpl) != 0)
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
                            //&& parameters[4].ParameterType == Type.GetType("Interop.BOOL", false)
                            )
                        {
                            GetActivationInfo = (delegate*<ObjectHandleOnStack, ref delegate*<void*, object>, ref void*, ref delegate*<object, void>, int*, void>)
                                met.MethodHandle.GetFunctionPointer();
                            IsSupported = true;
                        }
                    }
                }
            }

            internal ref struct ObjectHandleOnStack
            {
                private unsafe void* _ptr;

                public unsafe ObjectHandleOnStack(void* pObject) => _ptr = pObject;
            }
        }

        public static unsafe class ClrAllocator<T>
        {
            private static readonly delegate*<void*, T> _pfnAllocator;

            private static readonly void* _allocatorFirstArg;

            private static readonly delegate*<T, void> _pfnCtor;

            static ClrAllocator()
            {
                if (!ClrAllocator.IsSupported) return;

                var type = typeof(T);
                int _ctorIsPublic = default;
                ((delegate*<ClrAllocator.ObjectHandleOnStack, ref delegate*<void*, T>, ref void*, ref delegate*<T, void>, int*, void>)ClrAllocator.GetActivationInfo)
                (new ClrAllocator.ObjectHandleOnStack(Unsafe.AsPointer(ref type)), ref _pfnAllocator, ref _allocatorFirstArg, ref _pfnCtor, &_ctorIsPublic);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static T CreateInstance()
            {
                T result = _pfnAllocator(_allocatorFirstArg);
                _pfnCtor(result);
                return result;
            }
        }

        static class FastNewIL<T>
        {
            public static ConstructorInfo? CachedConstructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);

            public static Func<T> CompiledDelegate;

            public static bool IsValid = typeof(T).IsValueType || (FastNewIL<T>.CachedConstructor != null && !typeof(T).IsAbstract);

            static FastNewIL()
            {
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

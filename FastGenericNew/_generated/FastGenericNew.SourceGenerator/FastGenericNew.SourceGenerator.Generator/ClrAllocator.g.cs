﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by FastGenericNew.SourceGenerator
//     Please do not modify this file directly
// <auto-generated/>
//------------------------------------------------------------------------------
#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.ComponentModel;

#if NET6_0_OR_GREATER
namespace @FastGenericNew
{
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    static unsafe class ClrAllocator
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
    }

    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    static unsafe class ClrAllocator<T>
    {
        private static readonly delegate*<void*, T> _pfnAllocator;

        private static readonly void* _allocatorFirstArg;

        private static readonly delegate*<T, void> _pfnCtor;

        static ClrAllocator()
        {
            if (!ClrAllocator.IsSupported) return;

            var type = typeof(T);
            if (type.IsAbstract) goto smartThrow;

            int _ctorIsPublic = default;
            ((delegate*<void*, ref delegate*<void*, T>, ref void*, ref delegate*<T, void>, int*, void>)global::@FastGenericNew.ClrAllocator.GetActivationInfo)
            (Unsafe.AsPointer(ref type), ref _pfnAllocator, ref _allocatorFirstArg, ref _pfnCtor, &_ctorIsPublic);
            if (_pfnAllocator is null || _allocatorFirstArg is null || _pfnCtor is null)
                goto smartThrow;
            return;
smartThrow:
            _pfnAllocator = &SmartThrow;

            [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.NoInlining | global::System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
            static T SmartThrow(void* _) => global::@FastGenericNew.ThrowHelper.SmartThrowImpl<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CreateInstance()
        {
            T result = _pfnAllocator(_allocatorFirstArg);
            _pfnCtor(result);
            return result;
        }
    }
}
#endif

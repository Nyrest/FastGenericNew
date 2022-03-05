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
using System.ComponentModel;

namespace @FastGenericNew
{
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T>
    {
#if NETFRAMEWORK
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static readonly bool _isValueTypeT = typeof(T).IsValueType;
#endif

        public static readonly System.Linq.Expressions.Expression<Func<T>> SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<T>>(typeof(T).IsValueType
            ? (global::@FastGenericNew.FastNew<T>.Constructor != null
                ? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(global::@FastGenericNew.FastNew<T>.Constructor)
                : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(typeof(T)))
            : ((global::@FastGenericNew.FastNew<T>.Constructor != null && !typeof(T).IsAbstract)
                ? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(global::@FastGenericNew.FastNew<T>.Constructor)
                : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo))))
            , Array.Empty<System.Linq.Expressions.ParameterExpression>());

	    public static readonly Func<T> CompiledDelegate = SourceExpression.Compile();
    
        public static readonly bool IsValid = typeof(T).IsValueType || (global::@FastGenericNew.FastNew<T>.Constructor != null && !typeof(T).IsAbstract);
    }
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, T>> SourceExpression;

		public static readonly Func<TArg0, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			var p9 = System.Linq.Expressions.Expression.Parameter(typeof(TArg9));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			var p9 = System.Linq.Expressions.Expression.Parameter(typeof(TArg9));
			var p10 = System.Linq.Expressions.Expression.Parameter(typeof(TArg10));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			var p9 = System.Linq.Expressions.Expression.Parameter(typeof(TArg9));
			var p10 = System.Linq.Expressions.Expression.Parameter(typeof(TArg10));
			var p11 = System.Linq.Expressions.Expression.Parameter(typeof(TArg11));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			var p9 = System.Linq.Expressions.Expression.Parameter(typeof(TArg9));
			var p10 = System.Linq.Expressions.Expression.Parameter(typeof(TArg10));
			var p11 = System.Linq.Expressions.Expression.Parameter(typeof(TArg11));
			var p12 = System.Linq.Expressions.Expression.Parameter(typeof(TArg12));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			var p9 = System.Linq.Expressions.Expression.Parameter(typeof(TArg9));
			var p10 = System.Linq.Expressions.Expression.Parameter(typeof(TArg10));
			var p11 = System.Linq.Expressions.Expression.Parameter(typeof(TArg11));
			var p12 = System.Linq.Expressions.Expression.Parameter(typeof(TArg12));
			var p13 = System.Linq.Expressions.Expression.Parameter(typeof(TArg13));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			var p9 = System.Linq.Expressions.Expression.Parameter(typeof(TArg9));
			var p10 = System.Linq.Expressions.Expression.Parameter(typeof(TArg10));
			var p11 = System.Linq.Expressions.Expression.Parameter(typeof(TArg11));
			var p12 = System.Linq.Expressions.Expression.Parameter(typeof(TArg12));
			var p13 = System.Linq.Expressions.Expression.Parameter(typeof(TArg13));
			var p14 = System.Linq.Expressions.Expression.Parameter(typeof(TArg14));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 })).Compile();
		}
	}
	internal static partial class FastNew<
#if NET5_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
#endif
T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>
	{
		public static readonly System.Linq.Expressions.Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, T>> SourceExpression;

		public static readonly Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, T> CompiledDelegate;

		public static readonly bool IsValid;

		static FastNew()
		{
			var constructor = global::@FastGenericNew.FastNew<T, TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>.Constructor;
			IsValid = constructor != null && !typeof(T).IsAbstract;
			var p0 = System.Linq.Expressions.Expression.Parameter(typeof(TArg0));
			var p1 = System.Linq.Expressions.Expression.Parameter(typeof(TArg1));
			var p2 = System.Linq.Expressions.Expression.Parameter(typeof(TArg2));
			var p3 = System.Linq.Expressions.Expression.Parameter(typeof(TArg3));
			var p4 = System.Linq.Expressions.Expression.Parameter(typeof(TArg4));
			var p5 = System.Linq.Expressions.Expression.Parameter(typeof(TArg5));
			var p6 = System.Linq.Expressions.Expression.Parameter(typeof(TArg6));
			var p7 = System.Linq.Expressions.Expression.Parameter(typeof(TArg7));
			var p8 = System.Linq.Expressions.Expression.Parameter(typeof(TArg8));
			var p9 = System.Linq.Expressions.Expression.Parameter(typeof(TArg9));
			var p10 = System.Linq.Expressions.Expression.Parameter(typeof(TArg10));
			var p11 = System.Linq.Expressions.Expression.Parameter(typeof(TArg11));
			var p12 = System.Linq.Expressions.Expression.Parameter(typeof(TArg12));
			var p13 = System.Linq.Expressions.Expression.Parameter(typeof(TArg13));
			var p14 = System.Linq.Expressions.Expression.Parameter(typeof(TArg14));
			var p15 = System.Linq.Expressions.Expression.Parameter(typeof(TArg15));
			CompiledDelegate = (SourceExpression = System.Linq.Expressions.Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, T>>(IsValid
				? (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.New(constructor!, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15)
				: (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Call(global::@FastGenericNew.ThrowHelper.GetSmartThrow<T>(), System.Linq.Expressions.Expression.Constant(global::@FastGenericNew.FastNew<T>.Constructor, typeof(ConstructorInfo)))
			, new System.Linq.Expressions.ParameterExpression[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15 })).Compile();
		}
	}
}

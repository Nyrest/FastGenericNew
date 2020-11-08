using System;
using System.Linq.Expressions;
#pragma warning disable HAA0101 // Array allocation for params parameter

namespace FastGenericNew
{
    public static class FastNew<T>
    {
        public static readonly Func<T> CreateInstance
            = Expression.Lambda<Func<T>>(Expression.New(ConstructorOf<T>.value)).Compile();
    }

    public static class FastNew<T, TParameter>
    {
        public static readonly Func<TParameter, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            CreateInstance = Expression.Lambda<Func<TParameter, T>>(Expression.New(
                ConstructorOf<T, TParameter>.value,
                parameter1),
                parameter1).Compile();
        }
    }


    public static class FastNew<T, TParameter, TParameter2>
    {
        public static readonly Func<TParameter, TParameter2, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2>.value,
                parameter1, parameter2),
                parameter1, parameter2).Compile();
        }
    }


    public static class FastNew<T, TParameter, TParameter2, TParameter3>
    {
        public static readonly Func<TParameter, TParameter2, TParameter3, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            ParameterExpression parameter3 = Expression.Parameter(typeof(TParameter3));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, TParameter3, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2, TParameter3>.value,
                parameter1, parameter2, parameter3),
                parameter1, parameter2, parameter3).Compile();
        }
    }


    public static class FastNew<T, TParameter, TParameter2, TParameter3, TParameter4>
    {
        public static readonly Func<TParameter, TParameter2, TParameter3, TParameter4, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            ParameterExpression parameter3 = Expression.Parameter(typeof(TParameter3));
            ParameterExpression parameter4 = Expression.Parameter(typeof(TParameter4));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, TParameter3, TParameter4, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2, TParameter3, TParameter4>.value,
                parameter1, parameter2, parameter3, parameter4),
                parameter1, parameter2, parameter3, parameter4).Compile();
        }
    }

    public static class FastNew<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5>
    {
        public static readonly Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, T> CreateInstance;
        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            ParameterExpression parameter3 = Expression.Parameter(typeof(TParameter3));
            ParameterExpression parameter4 = Expression.Parameter(typeof(TParameter4));
            ParameterExpression parameter5 = Expression.Parameter(typeof(TParameter5));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5>.value,
                parameter1, parameter2, parameter3, parameter4, parameter5),
                parameter1, parameter2, parameter3, parameter4, parameter5).Compile();
        }
    }

    public static class FastNew<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>
    {
        public static readonly Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            ParameterExpression parameter3 = Expression.Parameter(typeof(TParameter3));
            ParameterExpression parameter4 = Expression.Parameter(typeof(TParameter4));
            ParameterExpression parameter5 = Expression.Parameter(typeof(TParameter5));
            ParameterExpression parameter6 = Expression.Parameter(typeof(TParameter6));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>.value,
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6),
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6).Compile();
        }
    }


    public static class FastNew<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>
    {
        public static readonly Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            ParameterExpression parameter3 = Expression.Parameter(typeof(TParameter3));
            ParameterExpression parameter4 = Expression.Parameter(typeof(TParameter4));
            ParameterExpression parameter5 = Expression.Parameter(typeof(TParameter5));
            ParameterExpression parameter6 = Expression.Parameter(typeof(TParameter6));
            ParameterExpression parameter7 = Expression.Parameter(typeof(TParameter7));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>.value,
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7),
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7).Compile();
        }
    }

    public static class FastNew<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>
    {
        public static readonly Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            ParameterExpression parameter3 = Expression.Parameter(typeof(TParameter3));
            ParameterExpression parameter4 = Expression.Parameter(typeof(TParameter4));
            ParameterExpression parameter5 = Expression.Parameter(typeof(TParameter5));
            ParameterExpression parameter6 = Expression.Parameter(typeof(TParameter6));
            ParameterExpression parameter7 = Expression.Parameter(typeof(TParameter7));
            ParameterExpression parameter8 = Expression.Parameter(typeof(TParameter8));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>.value,
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8),
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8).Compile();
        }
    }

    public static class FastNew<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9>
    {
        public static readonly Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9, T> CreateInstance;

        static FastNew()
        {
            ParameterExpression parameter1 = Expression.Parameter(typeof(TParameter));
            ParameterExpression parameter2 = Expression.Parameter(typeof(TParameter2));
            ParameterExpression parameter3 = Expression.Parameter(typeof(TParameter3));
            ParameterExpression parameter4 = Expression.Parameter(typeof(TParameter4));
            ParameterExpression parameter5 = Expression.Parameter(typeof(TParameter5));
            ParameterExpression parameter6 = Expression.Parameter(typeof(TParameter6));
            ParameterExpression parameter7 = Expression.Parameter(typeof(TParameter7));
            ParameterExpression parameter8 = Expression.Parameter(typeof(TParameter8));
            ParameterExpression parameter9 = Expression.Parameter(typeof(TParameter9));
            CreateInstance = Expression.Lambda<Func<TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9, T>>(Expression.New(
                ConstructorOf<T, TParameter, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9>.value,
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9),
                parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9).Compile();
        }
    }
}

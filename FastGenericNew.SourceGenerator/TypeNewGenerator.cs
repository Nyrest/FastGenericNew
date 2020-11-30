using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;
using static FastGenericNew.SourceGenerator.CompileSettings;
// TODO: Optimize this messed trash

namespace FastGenericNew.SourceGenerator
{
    [Generator]
    public class TypeNewGenerator : ISourceGenerator
    {
        public static Dictionary<Type, Func<object>> createInstanceCaches = new Dictionary<Type, Func<object>>();

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder sourceBuilder = new StringBuilder(@"
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace FastGenericNew {
    public static partial class TypeNew
    {
        public static Func<T> GetCreateInstance<T>(Type type) => 
            !type.IsValueType
            ? (Func<T>)
              typeof(FastNew<>)
              .MakeGenericType(type)
              .GetField(""CreateInstance"")
              .GetValue(null)
            : () => default(T);

        public static Func<object> GetCreateInstance(Type type)
        {
            if(!type.IsValueType)
            {
                return (Func<object>)
                    typeof(FastNew<>)
                    .MakeGenericType(type)
                    .GetField(""CreateInstance"")
                    .GetValue(null);
            }
            if (createInstanceCaches.TryGetValue(type, out var result))
                return result;
            createInstanceCaches.Add(type, 
                result = Expression.Lambda<Func<object>>(Expression.Convert(Expression.New(type), typeof(object))).Compile());
            return result;
        }
");
            sourceBuilder.Append(publicTypeNewCreateInstanceCaches
                ? "public static Dictionary<Type, Func<object>> createInstanceCaches = new Dictionary<Type, Func<object>>();"
                : "internal static Dictionary<Type, Func<object>> createInstanceCaches = new Dictionary<Type, Func<object>>();"
                );
            StringBuilder methodTypeParamBuilder = new StringBuilder("Type arg0", 10 * maxParameterCount);
            StringBuilder methodTypeVariablesBuilder = new StringBuilder(10 * maxParameterCount);
            StringBuilder methodObjectsGenericBuilder = new StringBuilder(7 * maxParameterCount);
            StringBuilder methodUserGenericBuilder = new StringBuilder(13 * maxParameterCount);
            StringBuilder fastNewTypeCommasBuilder = new StringBuilder(maxParameterCount);

            for (int parameterIndex = 0; parameterIndex < maxParameterCount; parameterIndex++)
            {
                if (parameterIndex != 0)
                {
                    methodTypeParamBuilder.Append(",Type arg" + parameterIndex);
                    methodUserGenericBuilder.Append(",TParameter" + parameterIndex);
                }
                fastNewTypeCommasBuilder.Append(',');
                methodObjectsGenericBuilder.Append(",object");
                methodTypeVariablesBuilder.Append(",arg" + parameterIndex);

                string methodObjectsResult = methodObjectsGenericBuilder.ToString();
                string methodGenericResult = methodUserGenericBuilder.ToString();
                string fastNewTypeCommas = fastNewTypeCommasBuilder.ToString();
                string delegateType = $@"{(parameterIndex <= 15
? $"Func<TParameter0{methodGenericResult},T>"
: $"FastNew<T,TParameter0{methodGenericResult}>.FastNewDelegate")}";
                #region All Object
                /*
                sourceBuilder.Append($@"
public static Func<object{methodObjectsResult}> 
GetCreateInstance(Type type,{methodTypeParamBuilder}) =>
    (Func<object{methodObjectsResult}>)
    typeof(FastNew<{fastNewTypeCommas}>)
    .MakeGenericType(type{methodTypeVariablesBuilder})
    .GetField(""CreateInstance"")
    .GetValue(null);");
                */
                #endregion

                #region All Generic
                sourceBuilder.Append($@"
public static {delegateType} 
GetCreateInstance<T,TParameter0{methodGenericResult}>(Type type,{methodTypeParamBuilder}) =>
    ({delegateType})
    typeof(FastNew<{fastNewTypeCommas}>)
    .MakeGenericType(type{methodTypeVariablesBuilder})
    .GetField(""CreateInstance"")
    .GetValue(null);");
                #endregion
            }
            sourceBuilder.Append("}}");
            context.AddSource("typeNewGenerated", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required
        }
    }
}

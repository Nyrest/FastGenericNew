using System.Runtime.CompilerServices;

namespace FastGenericNew.SourceGenerator.InternalGenerator.Utilities;

internal static class AnalyzerHelper
{
    public static bool TryGetNamedArgument<T>(this AttributeData attr, string name, out T result)
    {
        Unsafe.SkipInit(out result);
        foreach (var item in attr.NamedArguments)
        {
            if (item.Key.Equals(name))
            {
                var typedConstant = item.Value;
                switch (typedConstant.Type!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
                {
                    case "bool" when typeof(T) == typeof(bool) && typedConstant.Value != null:
                        result = (T)typedConstant.Value!;
                        return true;
                    default:
                        break;
                }
                return false;
            }
        }
        return false;
    }
}
    
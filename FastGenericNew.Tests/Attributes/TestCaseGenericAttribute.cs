using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace FastGenericNew.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class TestCaseSourceGenericAttribute : TestCaseAttribute, ITestBuilder
    {
        public TestCaseSourceGenericAttribute(string sourceName) : base(sourceName)
        {
            SourceName = sourceName;
        }

        public TestCaseSourceGenericAttribute(Type type, string sourceName) : base(sourceName)
        {
            SourceContainerType = type;
            SourceName = sourceName;
        }

        public string SourceName { get; set; }
        public Type? SourceContainerType { get; set; }

        IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, Test? suite)
        {
            if (!method.IsGenericMethodDefinition)
                yield break;

            var containerType = SourceContainerType ?? method.MethodInfo.DeclaringType as Type;
            if (containerType is null)
                yield break;

            foreach (var member in containerType
                .GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.Name == SourceName)
                )
            {
                IEnumerable? enumerable;
                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        enumerable = (IEnumerable?)((FieldInfo)member).GetValue(null);
                        break;
                    case MemberTypes.Method:
                        enumerable = (IEnumerable?)((MethodInfo)member).Invoke(null, null);
                        break;
                    case MemberTypes.Property:
                        enumerable = (IEnumerable?)((PropertyInfo)member).GetValue(null);
                        break;
                    default: continue;
                }
                if (enumerable is null) continue;

                foreach (Type type in enumerable)
                {
                    var a = new TestMethod(method.MakeGenericMethod(type), suite);
                    a.Name = $"{a.Name}<{type.FullName}>";
                    yield return a;
                }
            }
        }
    }
}

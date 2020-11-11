using System.Reflection;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run(Assembly.GetCallingAssembly());
        }
    }
}
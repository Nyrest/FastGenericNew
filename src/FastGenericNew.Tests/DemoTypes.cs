using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace FastGenericNew.Tests
{
    public record class DemoClassNoParamlessCtor
    {
        public const int DefaultValue = 2;

        public int value = DefaultValue;

        public DemoClassNoParamlessCtor(int value) => this.value = value;
    }

    public record class DemoClassPrivateCtor
    {
        public const int DefaultValue = 2;

        public int value = DefaultValue;

        private DemoClassPrivateCtor() { }

        private DemoClassPrivateCtor(int val) => value = val;

        public static DemoClassPrivateCtor Create() => new();

        public static DemoClassPrivateCtor Create(int val) => new(val);
    }

    public record class DemoClass
    {
        public const int DefaultValue = 2;

        public int value = DefaultValue;

        public string? allValues = null;

        public int mode;

        public DemoClass() { }

        public DemoClass(int val)
        {
            mode = 1;
            value = val;
        }

        public DemoClass(int val, int val2)
        {
            mode = 2;
            allValues = string.Join(",", new[] { val, val2 }.Select(x => x.ToString()).ToArray());
        }

        public DemoClass(int val, int val2, int val3, int val4, int val5, int val6, int val7, int val8, int val9, int val10, int val11, int val12, int val13, int val14, int val15, int val16, int val17, int val18)
        {
            mode = 3;
            allValues = string.Join(",", new int[] { val, val2, val3, val4, val5, val6, val7, val8, val9, val10, val11, val12, val13, val14, val15, val16, val17, val18 }.Select(x => x.ToString()).ToArray());
        }
    }

    public record struct DemoStruct
    {
        public int value;
    }

    public record struct DemoStructParameterless
    {
        public const int ParameterlessValue = 999999;

        public int value;

        public DemoStructParameterless()
        {
            value = ParameterlessValue;
        }
    }
}

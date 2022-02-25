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
    }

    public record class DemoClass
    {
        public const int DefaultValue = 2;

        public int value = DefaultValue;

        public DemoClass() { }

        public DemoClass(int val) => value = val;
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

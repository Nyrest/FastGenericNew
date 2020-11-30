namespace FastGenericNew.Tests
{
    public interface TInterface { }

    public class TClass { }

    public sealed class TClassPrivateConstructor
    {
        public int i;
        public string text;
        private TClassPrivateConstructor(int i, string text)
        {
            this.i = i;
            this.text = text;
        }
    }

    public class TClassWithParam
    {
        public int i;
        public string text;
        public TClassWithParam(int i, string text)
        {
            this.i = i;
            this.text = text;
        }
    }

    public abstract class TAbstractClass { }

    public sealed class TDerivedClass : TAbstractClass { }

    public struct TStruct { }

    public struct TStructWithParam
    {
        public int i;
        public string text;
        public TStructWithParam(int i, string text)
        {
            this.i = i;
            this.text = text;
        }
    }

    public struct TStructPrivateConstructor
    {
        public int i;
        public string text;
        public TStructPrivateConstructor(int i, string text)
        {
            this.i = i;
            this.text = text;
        }
    }
}

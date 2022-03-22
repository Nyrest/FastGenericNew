namespace FastGenericNew.SourceGenerator
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class GeneratorOptionAttribute : Attribute
    {
        public bool PresentPreProcessor { get; set; }

        public GeneratorOptionAttribute(object defaultValue) { }
    }
}
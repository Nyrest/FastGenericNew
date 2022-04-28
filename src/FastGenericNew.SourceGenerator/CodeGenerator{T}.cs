namespace FastGenericNew.SourceGenerator;

public abstract class CodeGenerator<TSelf> : CodeGenerator where TSelf : CodeGenerator<TSelf> { }
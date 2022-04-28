namespace FastGenericNew.SourceGenerator;

public class CodeGenerationResult
{
    public static readonly CodeGenerationResult Empty = new(string.Empty);

    public CodeGenerationResult(string filename, SourceText? sourceText = null, Diagnostic[]? diagnostics = null)
    {
        Filename = filename;
        SourceText = sourceText;
        Diagnostics = diagnostics;
    }

    public string Filename { get; private init; }

    public SourceText? SourceText { get; private init; }

    public Diagnostic[]? Diagnostics { get; private init; }
}

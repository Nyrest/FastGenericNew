namespace FastGenericNew.SourceGenerator.Utilities;

internal unsafe ref partial struct CodeBuilder
{
    private static readonly ArrayPool<char> pool = ArrayPool<char>.Shared;

    public List<Diagnostic>? Diagnostics { readonly get; private set; }

    private char[] _buffer;

    private int _length;

    public readonly Span<char> AvailableBuffer => _buffer.AsSpan(_length);

    public readonly GeneratorOptions Options { get; }

    public readonly int Length => _length;

    public readonly int BufferAvailable => _buffer.Length - _length;

    public readonly char LastChar => _length != 0 ? _buffer[_length - 1] : '\0';

    public CodeBuilder(int capacity, in GeneratorOptions options)
    {
        _buffer = pool.Rent(capacity);
        _length = 0;
        Diagnostics = null;
        Options = options;
    }

    #region Start & End
    public void StartNamespace()
    {
        if (string.IsNullOrWhiteSpace(Options.Namespace))
            return;
        Append("namespace @");
        AppendLine(Options.Namespace);
        AppendLine('{');
    }

    public void EndNamespace()
    {
        if (string.IsNullOrWhiteSpace(Options.Namespace)) return;
        AppendLine('}');
    }

    public void StartBlock(int indentCount)
    {
        Indent(indentCount);
        AppendLine('{');
    }

    public void EndBlock(int indentCount, bool newLine = true)
    {
        Indent(indentCount);
        if (newLine)
            AppendLine('}');
        else
            Append('}');
    }
    #endregion

    #region Indent
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Indent(int count)
    {
        if (!Options.PrettyOutput) return;
        switch (count)
        {
            case 0: break;
            case 1:
                Append('\t'); break;
            case 2:
                Append('\t', '\t'); break;
            case 3:
                Append("\t\t\t"); break;
            case 4:
                Append("\t\t\t\t"); break;
            default:
                Append(new string('\t', count)); break;
        }
    }
    #endregion

    #region Append Keywords
    public void AppendKeyword(string value)
    {
        Append(value);
        Append(' ');
    }

    public void AppendAccessibility(bool isPublic)
    {
        Append(isPublic ? "public " : "internal ");
    }
    #endregion

    #region Preprocessor

    public void Pre_If(string condition)
    {
        if (LastChar != '\n')
            AppendLine();
        Append("#if ");
        AppendLine(condition);
    }

    public void Pre_Else()
    {
        if (LastChar != '\n')
            AppendLine();
        AppendLine("#else");
    }

    public void Pre_ElseIf(string condition)
    {
        if (LastChar != '\n')
            AppendLine();
        Append("#elif ");
        AppendLine(condition);
    }

    public void Pre_EndIf()
    {
        if (LastChar != '\n')
            AppendLine();
        AppendLine("#endif");
    }

    #endregion

    public void XmlDoc(int indent, string value)
    {
        foreach (var line in value.AsSpan().Trim().Split('\n', false))
        {
            Indent(indent);
            AppendLine(line.TrimStart());
        }
    }

    public void Append(char value)
    {
        if (BufferAvailable == 0)
            Grow();
        _buffer[_length++] = value;
    }

    public void Append(char value, char value2)
    {
        if (BufferAvailable == 0)
            Grow();
        _buffer[_length] = value;
        _buffer[_length + 1] = value2;
        _length += 2;
    }

    public void Append(string value) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ReadOnlySpan<char> value)
    {
    tryAgain:
        if (!value.TryCopyTo(AvailableBuffer))
        {
            Grow();
            goto tryAgain;
        }
        _length += value.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(char value)
    {
        if (BufferAvailable <= 1)
            Grow();
        _buffer[_length] = value;
        _buffer[_length + 1] = '\n';
        _length += 2;
    }

    public void AppendLine(string value) => AppendLine(value.AsSpan());

    public void AppendLine(ReadOnlySpan<char> value)
    {
        Append(value);
        Append('\n');
    }

    public void AppendLine()
    {
        Append('\n');
    }

    public void PrettyNewLine()
    {
        if (Options.PrettyOutput)
            Append('\n');
    }

    public void AddDiagnostic(Diagnostic diagnostic)
    {
        if (Diagnostics is null) Diagnostics = new List<Diagnostic>();
        Diagnostics.Add(diagnostic);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Grow()
    {
        var oldBuffer = _buffer;
        _buffer = pool.Rent((_buffer.Length + 1) * 2);
        oldBuffer.CopyTo(_buffer, 0);
        pool.Return(oldBuffer);
    }

    public readonly CodeGenerationResult Build(CodeGenerator generator)
    {
        return new CodeGenerationResult(generator.Filename, SourceText.From(ToString(), Encoding.UTF8), Diagnostics?.ToArray());
    }

    public CodeGenerationResult BuildAndDispose(CodeGenerator generator)
    {
        var result = Build(generator);
        Dispose();
        return result;
    }

    public void Clear()
    {
        _length = 0;
    }

    public readonly override string ToString()
    {
        return _buffer.AsSpan(0, _length).ToString();
    }

    public void Dispose()
    {
        pool.Return(_buffer);
    }
}

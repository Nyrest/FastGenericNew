namespace FastGenericNew.SourceGenerator.Utilities;

partial struct CodeBuilder
{
    public void StartGeneric() => Append('<');

    public void EndGeneric() => Append('>');

    public void AppendGenericArgumentName(int argumentIndex)
    {
        Append("TArg");
        Append(argumentIndex.ToString());
    }

    public void AppendGenericMethodArgumentName(int argumentIndex)
    {
        Append('p');
        Append(argumentIndex.ToString());
    }

    public void DeclareGenericMember(int argumentCount, bool doNotAppendTrimmableAttr = false)
    {
        StartGeneric();

        if (Options.Trimmable && !doNotAppendTrimmableAttr)
        {
            Pre_If("NET5_0_OR_GREATER");
            AppendLine(Options.DynamicallyAccessedMembers(argumentCount));
            Pre_EndIf();
        }

        Append('T');
        for (int i = 0; i < argumentCount; i++)
        {
            Append(',', ' ');
            AppendGenericArgumentName(i);
        }
        EndGeneric();
    }

    public void DeclareFullGenericDelegate(int argumentCount)
    {
        StartGeneric();

        if (Options.Trimmable)
        {
            Pre_If("NET5_0_OR_GREATER");
            AppendLine(Options.DynamicallyAccessedMembers(argumentCount));
            Pre_EndIf();
        }

        Append("out T");
        for (int i = 0; i < argumentCount; i++)
        {
            Append(", in ");
            AppendGenericArgumentName(i);
        }
        EndGeneric();

        Append('(');
        for (int i = 0; i < argumentCount; i++)
        {
            if (i != 0)
            {
                Append(',', ' ');
            }
            AppendGenericArgumentName(i);
            Append(' ');
            AppendGenericMethodArgumentName(i);
        }
        Append(')', ';');
        AppendLine();
    }

    public void UseGenericDelegate(int argumentCount)
    {
        if (Options.ForceFastNewDelegate || argumentCount > 16)
        {
            GlobalNamespaceDot();
            Append($"FastNew.{FastNewDelegateGenerator.DelegateName}<T");
            for (int i = 0; i < argumentCount; i++)
            {
                Append(',', ' ');
                AppendGenericArgumentName(i);
            }
            Append(">");
        }
        else
        {
            Append("Func<");
            for (int i = 0; i < argumentCount; i++)
            {
                AppendGenericArgumentName(i);
                Append(',', ' ');
            }
            Append("T>");
        }
    }

    public void UseGenericMember(int argumentCount)
    {
        StartGeneric();
        Append('T');
        for (int i = 0; i < argumentCount; i++)
        {
            Append(',', ' ');
            AppendGenericArgumentName(i);
        }
        EndGeneric();
    }
}

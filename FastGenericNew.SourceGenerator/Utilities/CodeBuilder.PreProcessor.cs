using System.Diagnostics;

namespace FastGenericNew.SourceGenerator.Utilities;

partial struct CodeBuilder
{
	internal const string Const_PreProcessDefinePrefix = "FastNewPX_";


	public void Pre_AutoDefine(bool value, [CallerArgumentExpression("value")] string? expression = null)
	{
		if (!value) return;
		Debug.Assert(expression != null);
		if (LastChar != '\n')
			AppendLine();
		Append($"#define {Const_PreProcessDefinePrefix}");
		AppendLine(expression.AsSpan().Trim());
	}

	public void Pre_IfDefined(string definition) => Pre_If($"{Const_PreProcessDefinePrefix}{definition}");

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
}

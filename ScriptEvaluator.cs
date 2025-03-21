using System.Reflection;
using Mono.CSharp;

public class ScriptEvaluator : Evaluator
{
    internal TextWriter _textWriter;
	internal static StreamReportPrinter _reportPrinter;
	private readonly HashSet<string> StdLib = new(StringComparer.InvariantCultureIgnoreCase) { "mscorlib", "System.Core", "System", "System.Xml" };

	public ScriptEvaluator(TextWriter textWriter) : base(BuildContext(textWriter))
	{
		_textWriter = textWriter;
		ImportAppdomainAssemblies();
	}

	private void Reference(Assembly assembly)
	{
        string assemblyName = assembly.GetName().Name;

		if (assemblyName != "completions")
			ReferenceAssembly(assembly);
	}

	private static CompilerContext BuildContext(TextWriter textWriter)
	{
		_reportPrinter = new StreamReportPrinter(textWriter);

		CompilerSettings compilerSettings = new()
        {
			Version = LanguageVersion.Experimental,
			GenerateDebugInfo = false,
			StdLib = true,
			Target = Target.Library,
			WarningLevel = 0,
			EnhancedWarnings = false
		};

		return new CompilerContext(compilerSettings, _reportPrinter);
	}

	private void ImportAppdomainAssemblies()
	{
		foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			if (!StdLib.Contains(assembly.GetName().Name))
			{
				try
				{
					Reference(assembly);
				}
				catch { }
			}
		}
	}
}
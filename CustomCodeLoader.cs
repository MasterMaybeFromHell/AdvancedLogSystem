namespace AdvancedLogSystem
{
    public class CustomCodeLoader
    {
        private static StringWriter StringWriter = new();

        public static void LoadAndExecuteScripts(string path)
        {
            CreateDirectory(path);
            ProcessFilesInDirectory(path, (file) => ExecuteScript(file, StringWriter));
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AdvancedLogger.LogMessage($"[Custom Code] Directory created: {path}");
            }
        }

        private static void ProcessFilesInDirectory(string directoryPath, Action<string> action)
        {
            var files = Directory.GetFiles(directoryPath, "*.cs");

            foreach (var file in files)
                action(file);
        }

        private static void ExecuteScript(string filePath, TextWriter textWriter)
        {
            try
            {
                string code = ReadCodeFromFile(filePath);
                var evaluator = new ScriptEvaluator(textWriter);

                var executionResult = CompileAndExecute(evaluator, code);

                if (executionResult != null)
                {
                    AdvancedLogger.LogMessage($"[Custom Code] Script executed successfully: {filePath}");
                }
                else
                {
                    AdvancedLogger.LogMessage($"[Custom Code Warning] Script did not produce a result: {filePath}");
                }
            }
            catch (Exception ex)
            {
                AdvancedLogger.LogMessage($"[Custom Code Error] Failed to execute script '{filePath}': {ex.Message}");
            }
        }

        private static string ReadCodeFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            return File.ReadAllText(filePath);
        }

        private static Delegate CompileAndExecute(ScriptEvaluator evaluator, string code)
        {
            var compiledMethod = evaluator.Compile(code) ?? throw new InvalidOperationException("Failed to compile the script.");

            object result = null;
            compiledMethod(ref result);

            return compiledMethod;
        }
    }
}
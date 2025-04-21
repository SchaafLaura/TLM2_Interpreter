namespace TLM2_Interpreter;

internal static class Parser
{
    public static void Parse(string path)
    {
        ValidatePath(path);
        var lines = File.ReadAllLines(path);
        var funcs = ParseLines(lines);
        foreach (var (_, f) in funcs)
            f.Finalize();
    }

    private static Dictionary<string, TLMFunction> ParseLines(string[] lines)
    {
        var k = 0;
        var dict = new Dictionary<string, TLMFunction>();
        var line = string.Empty;
        while (k < lines.Length)
        {
            line = lines[k];
            switch (line)
            {
                case ['{', .. var signature]: ParseFunction(signature); break; // function
                default: throw new Exception($"Error parsing line {line}");
            }
            k++;
        }
        return dict;

        void ParseFunction(string signature)
        {
            var f = new TLMFunction(signature);
            dict.Add(f.Name, f);
            while (++k < lines.Length)
            {
                line = lines[k];
                if (line == "}") { return;}
                f.AddLine(line);
            }
            throw new Exception($"Could not find closing bracket for function {f.Name}");
        }
    }
    
    private static void ValidatePath(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        if (path.Length < 5)
            throw new ArgumentException($"\"{path}\" is not a valid filename or path");

        var extension = path[^4..];

        if (extension != ".tlm")
            throw new ArgumentException($"""File extension "{extension}" not valid. Should be ".tlm" """);
        
        if (!File.Exists(path))
            throw new IOException($"File/path {path} does not exist.");
    }
}
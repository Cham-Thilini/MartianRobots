using MartianRobots.Application;

// Composition root for the CLI.
//
// Usage:
//   martian-robots            accepts interactive input, or reads redirected stdin
//   martian-robots input.txt  reads the puzzle input from the given file
//
// Output (final robot positions) is written to stdout; errors go to stderr with a non-zero
// exit code, so the program composes cleanly in a shell pipeline.
try
{
    var input = ReadInput(args);

    var runner = SimulationRunner.CreateDefault();
    var output = runner.Run(input);

    Console.Out.WriteLine(output);
    return 0;
}
catch (Exception ex) when (ex is FormatException or ArgumentException or IOException)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}

static string ReadInput(string[] args)
{
    if (args.Length > 1)
        throw new ArgumentException("Too many arguments. Pass a single input file path, or none to read stdin.");

    if (args.Length == 1)
    {
        var path = args[0];
        if (!File.Exists(path))
            throw new IOException($"Input file not found: '{path}'.");
        return File.ReadAllText(path);
    }

    if (Console.IsInputRedirected)
        return Console.In.ReadToEnd();

    Console.WriteLine("Martian Robots simulator");
    Console.WriteLine("Enter the grid, then each robot's position and instructions.");
    Console.WriteLine("Submit with an empty line, or Ctrl+Z then Enter.");
    Console.WriteLine();

    var lines = new List<string>();
    while (true)
    {
        var line = Console.ReadLine();
        if (line is null || (line.Length == 0 && lines.Count > 0))
            break;

        var endOfInputIndex = line.IndexOf('\u001a');
        if (endOfInputIndex >= 0)
        {
            var textBeforeEndOfInput = line[..endOfInputIndex];
            if (textBeforeEndOfInput.Length > 0)
                lines.Add(textBeforeEndOfInput);

            break;
        }

        if (line.Length > 0)
            lines.Add(line);
    }

    return string.Join(Environment.NewLine, lines);
}

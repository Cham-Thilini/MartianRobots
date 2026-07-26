using MartianRobots.Application;

// Composition root for the CLI.
//
// Usage:
//   martian-robots            reads the puzzle input from stdin
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

    return Console.In.ReadToEnd();
}

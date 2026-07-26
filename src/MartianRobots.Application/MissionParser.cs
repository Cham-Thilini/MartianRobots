using MartianRobots.Domain;

namespace MartianRobots.Application;

/// <summary>
/// Parses the text format described in the brief:
/// <code>
///   maxX maxY          (first line: upper-right grid corner)
///   x y O              (a robot's start position + orientation)
///   LRF...             (that robot's instruction string)
///   ...                (repeated per robot)
/// </code>
/// Blank lines are ignored, so the parser tolerates the blank-line-separated layout some
/// versions of the brief use. It deliberately does <b>not</b> validate the instruction
/// letters — which characters are valid is the command factory's responsibility, so adding
/// a new command type never requires touching the parser.
/// </summary>
public sealed class MissionParser : IMissionParser
{
    /// <summary>Exclusive upper bound on instruction-string length, per the brief.</summary>
    public const int MaxInstructionLength = 100;

    private static readonly char[] Whitespace = { ' ', '\t' };

    public ParsedInput Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        // A UTF-8 byte-order mark can survive into a string when input arrives over stdin
        // (File.ReadAllText strips it, Console.In does not). Drop it so the first token parses.
        var lines = input
            .TrimStart('﻿')
            .Split('\n')
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .ToList();

        if (lines.Count == 0)
            throw new FormatException("Input is empty: expected a grid size on the first line.");

        var grid = ParseGrid(lines[0]);

        var missions = new List<RobotMission>();
        for (var i = 1; i < lines.Count; i += 2)
        {
            if (i + 1 >= lines.Count)
                throw new FormatException(
                    $"Robot starting '{lines[i]}' has no instruction line.");

            var start = ParseRobot(lines[i]);
            var instructions = ParseInstructions(lines[i + 1]);
            missions.Add(new RobotMission(start, instructions));
        }

        return new ParsedInput(grid, missions);
    }

    private static Grid ParseGrid(string line)
    {
        var parts = Split(line);
        if (parts.Length != 2)
            throw new FormatException($"Invalid grid line '{line}': expected 'maxX maxY'.");

        return new Grid(ParseInt(parts[0], "grid maxX"), ParseInt(parts[1], "grid maxY"));
    }

    private static RobotState ParseRobot(string line)
    {
        var parts = Split(line);
        if (parts.Length != 3)
            throw new FormatException($"Invalid robot line '{line}': expected 'x y orientation'.");

        var position = new Coordinates(ParseInt(parts[0], "robot x"), ParseInt(parts[1], "robot y"));

        if (parts[2].Length != 1)
            throw new FormatException($"Invalid orientation '{parts[2]}': expected a single letter.");

        return new RobotState(position, OrientationExtensions.FromSymbol(parts[2][0]));
    }

    private static string ParseInstructions(string line)
    {
        if (line.Length >= MaxInstructionLength)
            throw new FormatException(
                $"Instruction string is {line.Length} characters; must be fewer than {MaxInstructionLength}.");

        return line.ToUpperInvariant();
    }

    private static string[] Split(string line)
        => line.Split(Whitespace, StringSplitOptions.RemoveEmptyEntries);

    private static int ParseInt(string value, string field)
        => int.TryParse(value, out var result)
            ? result
            : throw new FormatException($"Invalid {field} value '{value}': expected an integer.");
}

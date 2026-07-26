using MartianRobots.Application;
using MartianRobots.Domain;

namespace MartianRobots.Tests.Application;

public class MissionParserTests
{
    private readonly MissionParser _parser = new();

    [Fact]
    public void Parses_grid_and_all_robots()
    {
        const string input = "5 3\n1 1 E\nRFRFRFRF\n3 2 N\nFRRFLLFFRRFLL";

        var result = _parser.Parse(input);

        Assert.True(result.Grid.Contains(new Coordinates(5, 3)));
        Assert.False(result.Grid.Contains(new Coordinates(6, 3)));
        Assert.Equal(2, result.Missions.Count);

        Assert.Equal(new RobotState(new Coordinates(1, 1), Orientation.East), result.Missions[0].Start);
        Assert.Equal("RFRFRFRF", result.Missions[0].Instructions);
    }

    [Fact]
    public void Ignores_blank_lines_between_robots()
    {
        const string input = "5 3\n\n1 1 E\nRFRFRFRF\n\n\n3 2 N\nFRRFLLFFRRFLL\n";

        var result = _parser.Parse(input);

        Assert.Equal(2, result.Missions.Count);
    }

    [Fact]
    public void Tolerates_windows_line_endings()
    {
        const string input = "5 3\r\n1 1 E\r\nRFRFRFRF\r\n";

        var result = _parser.Parse(input);

        Assert.Single(result.Missions);
        Assert.Equal("RFRFRFRF", result.Missions[0].Instructions);
    }

    [Fact]
    public void Lowercase_instructions_are_normalised_to_uppercase()
    {
        var result = _parser.Parse("5 3\n1 1 E\nrflf");
        Assert.Equal("RFLF", result.Missions[0].Instructions);
    }

    [Fact]
    public void Strips_a_leading_utf8_byte_order_mark()
    {
        var result = _parser.Parse("﻿5 3\n1 1 E\nF");
        Assert.True(result.Grid.Contains(new Coordinates(5, 3)));
        Assert.Single(result.Missions);
    }

    [Fact]
    public void Empty_input_throws()
        => Assert.Throws<FormatException>(() => _parser.Parse("   \n  \n"));

    [Fact]
    public void Robot_without_an_instruction_line_throws()
        => Assert.Throws<FormatException>(() => _parser.Parse("5 3\n1 1 E"));

    [Theory]
    [InlineData("5\n1 1 E\nF")]        // grid line has one value
    [InlineData("5 3 1\n1 1 E\nF")]    // grid line has three values
    [InlineData("x 3\n1 1 E\nF")]      // non-integer grid value
    public void Malformed_grid_line_throws(string input)
        => Assert.Throws<FormatException>(() => _parser.Parse(input));

    [Theory]
    [InlineData("5 3\n1 1\nF")]        // robot line missing orientation
    [InlineData("5 3\n1 1 Q\nF")]      // unknown orientation
    [InlineData("5 3\na 1 E\nF")]      // non-integer coordinate
    public void Malformed_robot_line_throws(string input)
        => Assert.Throws<FormatException>(() => _parser.Parse(input));

    [Fact]
    public void Instruction_string_at_or_above_the_length_limit_throws()
    {
        var tooLong = new string('F', MissionParser.MaxInstructionLength);
        Assert.Throws<FormatException>(() => _parser.Parse($"5 3\n1 1 E\n{tooLong}"));
    }
}

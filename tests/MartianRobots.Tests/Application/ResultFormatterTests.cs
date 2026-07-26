using MartianRobots.Application;
using MartianRobots.Domain;

namespace MartianRobots.Tests.Application;

public class ResultFormatterTests
{
    private readonly ResultFormatter _formatter = new();

    [Fact]
    public void Formats_a_surviving_robot_as_position_and_orientation()
    {
        var output = _formatter.Format(new[]
        {
            new RobotState(new Coordinates(1, 1), Orientation.East),
        });

        Assert.Equal("1 1 E", output);
    }

    [Fact]
    public void Appends_LOST_for_a_lost_robot()
    {
        var output = _formatter.Format(new[]
        {
            new RobotState(new Coordinates(3, 3), Orientation.North, IsLost: true),
        });

        Assert.Equal("3 3 N LOST", output);
    }

    [Fact]
    public void Writes_one_robot_per_line()
    {
        var output = _formatter.Format(new[]
        {
            new RobotState(new Coordinates(1, 1), Orientation.East),
            new RobotState(new Coordinates(3, 3), Orientation.North, IsLost: true),
            new RobotState(new Coordinates(2, 3), Orientation.South),
        });

        Assert.Equal("1 1 E\n3 3 N LOST\n2 3 S", output);
    }

    [Fact]
    public void Empty_result_set_produces_empty_string()
        => Assert.Equal(string.Empty, _formatter.Format(Array.Empty<RobotState>()));
}

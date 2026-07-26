using MartianRobots.Domain;
using MartianRobots.Domain.Commands;

namespace MartianRobots.Tests.Domain;

public class CommandTests
{
    private static readonly Coordinates Origin = new(2, 2);

    [Fact]
    public void TurnLeft_rotates_without_moving()
    {
        var state = new RobotState(Origin, Orientation.North);
        var result = new TurnLeftCommand().Execute(state, new FakeMarsWorld());

        Assert.Equal(new RobotState(Origin, Orientation.West), result);
    }

    [Fact]
    public void TurnRight_rotates_without_moving()
    {
        var state = new RobotState(Origin, Orientation.North);
        var result = new TurnRightCommand().Execute(state, new FakeMarsWorld());

        Assert.Equal(new RobotState(Origin, Orientation.East), result);
    }

    [Fact]
    public void Forward_moves_one_point_when_the_target_is_in_bounds()
    {
        var target = new Coordinates(2, 3);
        var world = new FakeMarsWorld().WithInBounds(target);
        var state = new RobotState(Origin, Orientation.North);

        var result = new MoveForwardCommand().Execute(state, world);

        Assert.Equal(new RobotState(target, Orientation.North), result);
        Assert.False(result.IsLost);
    }

    [Fact]
    public void Forward_off_a_clean_edge_loses_the_robot_and_leaves_a_scent()
    {
        // Target (2,3) is NOT registered as in-bounds -> it is off the grid.
        var world = new FakeMarsWorld();
        var state = new RobotState(Origin, Orientation.North);

        var result = new MoveForwardCommand().Execute(state, world);

        Assert.True(result.IsLost);
        Assert.Equal(Origin, result.Position); // last cell occupied before falling off
        Assert.Equal(Orientation.North, result.Orientation);
        Assert.Contains(Origin, world.MarkedScents);
    }

    [Fact]
    public void Forward_off_a_scented_edge_is_ignored()
    {
        // The fatal cell already carries a scent from a previous robot.
        var world = new FakeMarsWorld().WithScentAt(Origin);
        var state = new RobotState(Origin, Orientation.North);

        var result = new MoveForwardCommand().Execute(state, world);

        Assert.False(result.IsLost);
        Assert.Equal(state, result);               // robot stays exactly where it was
        Assert.Empty(world.MarkedScents);          // no new scent added
    }
}

using MartianRobots.Application;
using MartianRobots.Domain;

namespace MartianRobots.Tests.Application;

public class RobotSimulatorTests
{
    private readonly RobotSimulator _simulator = new(RobotCommandFactory.CreateDefault());

    [Fact]
    public void Runs_a_full_instruction_string()
    {
        var world = new MarsWorld(new Grid(5, 3));
        var mission = new RobotMission(new RobotState(new Coordinates(1, 1), Orientation.East), "RFRFRFRF");

        var result = _simulator.Run(mission, world);

        Assert.Equal(new RobotState(new Coordinates(1, 1), Orientation.East), result);
    }

    [Fact]
    public void Stops_processing_once_the_robot_is_lost()
    {
        var world = new MarsWorld(new Grid(5, 3));
        // Faces North at the top edge and is told to go forward, then execute more turns.
        // Everything after the fatal F must be ignored, so the orientation stays North.
        var mission = new RobotMission(new RobotState(new Coordinates(1, 3), Orientation.North), "FLLLL");

        var result = _simulator.Run(mission, world);

        Assert.True(result.IsLost);
        Assert.Equal(Orientation.North, result.Orientation);
        Assert.Equal(new Coordinates(1, 3), result.Position);
    }

    [Fact]
    public void Scent_from_one_robot_protects_the_next_over_the_shared_world()
    {
        var world = new MarsWorld(new Grid(5, 3));

        var first = _simulator.Run(
            new RobotMission(new RobotState(new Coordinates(1, 3), Orientation.North), "F"), world);
        Assert.True(first.IsLost); // leaves a scent at (1,3)

        var second = _simulator.Run(
            new RobotMission(new RobotState(new Coordinates(1, 3), Orientation.North), "F"), world);

        Assert.False(second.IsLost); // saved by the scent
        Assert.Equal(new Coordinates(1, 3), second.Position);
    }

    [Fact]
    public void Empty_instruction_string_leaves_the_robot_untouched()
    {
        var world = new MarsWorld(new Grid(5, 3));
        var start = new RobotState(new Coordinates(2, 2), Orientation.South);

        var result = _simulator.Run(new RobotMission(start, string.Empty), world);

        Assert.Equal(start, result);
    }
}

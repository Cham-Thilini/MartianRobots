namespace MartianRobots.Domain.Commands;

/// <summary>Turns the robot 90° to the left. Position is unchanged.</summary>
public sealed class TurnLeftCommand : IRobotCommand
{
    public RobotState Execute(RobotState state, IMarsWorld world)
        => state with { Orientation = state.Orientation.TurnLeft() };
}

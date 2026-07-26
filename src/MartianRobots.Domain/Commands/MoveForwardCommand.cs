namespace MartianRobots.Domain.Commands;

/// <summary>
/// Moves the robot one grid point forward. This command owns the two subtle rules of the
/// problem:
/// <list type="bullet">
///   <item>Driving off the edge loses the robot forever, leaving a scent at its last cell.</item>
///   <item>A move that would drive off a cell that already carries a scent is ignored.</item>
/// </list>
/// </summary>
public sealed class MoveForwardCommand : IRobotCommand
{
    public RobotState Execute(RobotState state, IMarsWorld world)
    {
        var target = state.Position.Step(state.Orientation);

        if (world.IsInBounds(target))
            return state with { Position = target };

        // The move would leave the grid. If an earlier robot was already lost here, its
        // scent saves this one: the fatal instruction is ignored and the robot stays put.
        if (world.HasScent(state.Position))
            return state;

        // Otherwise the robot is lost. Leave a scent at the last occupied cell.
        world.MarkScent(state.Position);
        return state with { IsLost = true };
    }
}

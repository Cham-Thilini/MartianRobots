using MartianRobots.Domain;

namespace MartianRobots.Application;

/// <summary>
/// Runs a single robot's instruction string against the shared world. Instructions are
/// applied in order and execution stops the moment the robot is lost — a lost robot ignores
/// any remaining instructions.
/// </summary>
public sealed class RobotSimulator
{
    private readonly IRobotCommandFactory _commandFactory;

    public RobotSimulator(IRobotCommandFactory commandFactory)
        => _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

    public RobotState Run(RobotMission mission, IMarsWorld world)
    {
        ArgumentNullException.ThrowIfNull(mission);
        ArgumentNullException.ThrowIfNull(world);

        var state = mission.Start;
        foreach (var token in mission.Instructions)
        {
            var command = _commandFactory.Create(token);
            state = command.Execute(state, world);

            if (state.IsLost)
                break;
        }

        return state;
    }
}

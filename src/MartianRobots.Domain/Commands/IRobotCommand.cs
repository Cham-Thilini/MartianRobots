namespace MartianRobots.Domain.Commands;

/// <summary>
/// A single robot instruction. This is the extension point the brief calls for
/// ("additional command types may be required in the future"): a new command is a new
/// class implementing this interface, registered in the command factory — no existing
/// code changes (Open/Closed Principle).
/// </summary>
public interface IRobotCommand
{
    /// <summary>
    /// Applies the command to <paramref name="state"/> and returns the resulting state.
    /// Implementations are pure with respect to the robot: they never mutate the incoming
    /// state, they return a new one. The <paramref name="world"/> may be queried and, in the
    /// case of a fatal move, updated with a scent.
    /// </summary>
    RobotState Execute(RobotState state, IMarsWorld world);
}

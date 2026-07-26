namespace MartianRobots.Domain;

/// <summary>
/// The complete state of a robot at a point in time: where it is, which way it faces, and
/// whether it has been lost over the edge. Immutable — every command returns a new state,
/// which keeps command logic pure and trivial to unit test.
/// </summary>
/// <param name="Position">The robot's grid coordinate. For a lost robot this is the last cell it occupied.</param>
/// <param name="Orientation">The direction the robot is facing.</param>
/// <param name="IsLost">True once the robot has driven off the edge of the grid.</param>
public sealed record RobotState(Coordinates Position, Orientation Orientation, bool IsLost = false);

namespace MartianRobots.Domain;

/// <summary>
/// Compass orientation. The declaration order is clockwise (N → E → S → W) on purpose:
/// it makes turning left/right simple modular arithmetic in <see cref="OrientationExtensions"/>.
/// </summary>
public enum Orientation
{
    North = 0,
    East = 1,
    South = 2,
    West = 3,
}

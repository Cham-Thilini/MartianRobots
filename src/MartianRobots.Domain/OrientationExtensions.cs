namespace MartianRobots.Domain;

/// <summary>
/// Turning, movement deltas, and symbol conversion for <see cref="Orientation"/>.
/// Kept as extension methods so <see cref="Orientation"/> stays a plain, serialisable enum.
/// </summary>
public static class OrientationExtensions
{
    private const int CompassPoints = 4;

    /// <summary>Turn 90° anticlockwise (equivalent to +3 mod 4 in clockwise ordering).</summary>
    public static Orientation TurnLeft(this Orientation orientation)
        => (Orientation)(((int)orientation + CompassPoints - 1) % CompassPoints);

    /// <summary>Turn 90° clockwise.</summary>
    public static Orientation TurnRight(this Orientation orientation)
        => (Orientation)(((int)orientation + 1) % CompassPoints);

    /// <summary>The unit step (dx, dy) for moving forward in this orientation.</summary>
    public static (int dx, int dy) ToDelta(this Orientation orientation) => orientation switch
    {
        Orientation.North => (0, 1),
        Orientation.East => (1, 0),
        Orientation.South => (0, -1),
        Orientation.West => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, "Unknown orientation."),
    };

    /// <summary>The single-letter symbol used in the input/output format.</summary>
    public static char ToSymbol(this Orientation orientation) => orientation switch
    {
        Orientation.North => 'N',
        Orientation.East => 'E',
        Orientation.South => 'S',
        Orientation.West => 'W',
        _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, "Unknown orientation."),
    };

    /// <summary>Parses a single-letter orientation symbol (case-insensitive).</summary>
    public static Orientation FromSymbol(char symbol) => char.ToUpperInvariant(symbol) switch
    {
        'N' => Orientation.North,
        'E' => Orientation.East,
        'S' => Orientation.South,
        'W' => Orientation.West,
        _ => throw new FormatException($"Unknown orientation symbol '{symbol}'."),
    };
}

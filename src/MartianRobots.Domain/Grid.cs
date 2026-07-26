namespace MartianRobots.Domain;

/// <summary>
/// The rectangular Martian world. The lower-left corner is fixed at (0, 0); only the
/// upper-right corner is configurable, matching the problem's input format.
/// </summary>
public sealed class Grid
{
    /// <summary>The maximum coordinate value permitted by the brief.</summary>
    public const int MaxAllowedCoordinate = 50;

    public int MaxX { get; }
    public int MaxY { get; }

    public Grid(int maxX, int maxY)
    {
        if (maxX < 0 || maxY < 0)
            throw new ArgumentException($"Grid bounds must be non-negative, but were ({maxX}, {maxY}).");
        if (maxX > MaxAllowedCoordinate || maxY > MaxAllowedCoordinate)
            throw new ArgumentException(
                $"Grid bounds must not exceed {MaxAllowedCoordinate}, but were ({maxX}, {maxY}).");

        MaxX = maxX;
        MaxY = maxY;
    }

    /// <summary>True if the coordinate lies within the grid (inclusive of the edges).</summary>
    public bool Contains(Coordinates coordinates)
        => coordinates.X >= 0 && coordinates.Y >= 0
        && coordinates.X <= MaxX && coordinates.Y <= MaxY;
}

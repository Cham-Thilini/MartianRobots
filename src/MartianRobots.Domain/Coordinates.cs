namespace MartianRobots.Domain;

/// <summary>
/// An immutable grid coordinate (x, y). Value-equality comes for free from the record struct,
/// which is what lets us store scent positions in a <see cref="System.Collections.Generic.HashSet{T}"/>.
/// </summary>
public readonly record struct Coordinates(int X, int Y)
{
    /// <summary>Returns the coordinate one grid point away in the given orientation.</summary>
    public Coordinates Step(Orientation orientation)
    {
        var (dx, dy) = orientation.ToDelta();
        return new Coordinates(X + dx, Y + dy);
    }

    public override string ToString() => $"{X} {Y}";
}

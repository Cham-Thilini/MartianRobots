using MartianRobots.Domain;

namespace MartianRobots.Tests.Domain;

/// <summary>
/// A hand-rolled <see cref="IMarsWorld"/> for unit-testing commands in isolation. Bounds and
/// scents are configured explicitly so each command test controls exactly one variable.
/// </summary>
internal sealed class FakeMarsWorld : IMarsWorld
{
    private readonly HashSet<Coordinates> _inBounds = new();
    private readonly HashSet<Coordinates> _scents = new();

    public List<Coordinates> MarkedScents { get; } = new();

    public FakeMarsWorld WithInBounds(params Coordinates[] coordinates)
    {
        foreach (var c in coordinates) _inBounds.Add(c);
        return this;
    }

    public FakeMarsWorld WithScentAt(params Coordinates[] coordinates)
    {
        foreach (var c in coordinates) _scents.Add(c);
        return this;
    }

    public bool IsInBounds(Coordinates coordinates) => _inBounds.Contains(coordinates);

    public bool HasScent(Coordinates coordinates) => _scents.Contains(coordinates);

    public void MarkScent(Coordinates coordinates)
    {
        _scents.Add(coordinates);
        MarkedScents.Add(coordinates);
    }
}

namespace MartianRobots.Domain;

/// <summary>
/// The default <see cref="IMarsWorld"/>: a <see cref="Grid"/> together with the set of cells
/// that carry a scent. A single instance is shared by every robot in one mission so that a
/// scent left by an earlier robot protects later ones.
/// </summary>
public sealed class MarsWorld : IMarsWorld
{
    private readonly Grid _grid;
    private readonly HashSet<Coordinates> _scentedCells = new();

    public MarsWorld(Grid grid)
        => _grid = grid ?? throw new ArgumentNullException(nameof(grid));

    public bool IsInBounds(Coordinates coordinates) => _grid.Contains(coordinates);

    public bool HasScent(Coordinates coordinates) => _scentedCells.Contains(coordinates);

    public void MarkScent(Coordinates coordinates) => _scentedCells.Add(coordinates);
}

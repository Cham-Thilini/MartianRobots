namespace MartianRobots.Domain;

/// <summary>
/// The environment a command interacts with while a robot executes: grid bounds plus the
/// "scent" left behind by robots that were previously lost. Commands depend on this
/// abstraction rather than a concrete world (Dependency Inversion), which keeps them
/// isolated and easy to test with a fake.
/// </summary>
public interface IMarsWorld
{
    /// <summary>True if the coordinate is inside the grid.</summary>
    bool IsInBounds(Coordinates coordinates);

    /// <summary>True if a robot was previously lost from this exact cell.</summary>
    bool HasScent(Coordinates coordinates);

    /// <summary>Records a scent at the cell a robot was lost from.</summary>
    void MarkScent(Coordinates coordinates);
}

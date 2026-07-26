using MartianRobots.Domain;

namespace MartianRobots.Tests.Domain;

public class GridTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(5, 3)]
    [InlineData(2, 2)]
    public void Contains_is_true_for_points_on_or_inside_the_edges(int x, int y)
        => Assert.True(new Grid(5, 3).Contains(new Coordinates(x, y)));

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(6, 3)]
    [InlineData(5, 4)]
    public void Contains_is_false_beyond_the_edges(int x, int y)
        => Assert.False(new Grid(5, 3).Contains(new Coordinates(x, y)));

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    public void Constructor_rejects_negative_bounds(int maxX, int maxY)
        => Assert.Throws<ArgumentException>(() => new Grid(maxX, maxY));

    [Theory]
    [InlineData(51, 0)]
    [InlineData(0, 51)]
    public void Constructor_rejects_bounds_above_the_maximum(int maxX, int maxY)
        => Assert.Throws<ArgumentException>(() => new Grid(maxX, maxY));

    [Fact]
    public void Constructor_accepts_the_maximum_bound()
    {
        var grid = new Grid(Grid.MaxAllowedCoordinate, Grid.MaxAllowedCoordinate);
        Assert.True(grid.Contains(new Coordinates(50, 50)));
    }
}

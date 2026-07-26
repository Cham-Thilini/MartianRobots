using MartianRobots.Domain;

namespace MartianRobots.Tests.Domain;

public class CoordinatesTests
{
    [Theory]
    [InlineData(2, 2, Orientation.North, 2, 3)]
    [InlineData(2, 2, Orientation.East, 3, 2)]
    [InlineData(2, 2, Orientation.South, 2, 1)]
    [InlineData(2, 2, Orientation.West, 1, 2)]
    public void Step_moves_one_point_in_the_facing_direction(
        int x, int y, Orientation orientation, int expectedX, int expectedY)
    {
        var result = new Coordinates(x, y).Step(orientation);
        Assert.Equal(new Coordinates(expectedX, expectedY), result);
    }

    [Fact]
    public void Equality_is_by_value()
        => Assert.Equal(new Coordinates(1, 2), new Coordinates(1, 2));

    [Fact]
    public void ToString_uses_space_separated_format()
        => Assert.Equal("3 4", new Coordinates(3, 4).ToString());
}

using MartianRobots.Domain;

namespace MartianRobots.Tests.Domain;

public class OrientationExtensionsTests
{
    [Theory]
    [InlineData(Orientation.North, Orientation.West)]
    [InlineData(Orientation.West, Orientation.South)]
    [InlineData(Orientation.South, Orientation.East)]
    [InlineData(Orientation.East, Orientation.North)]
    public void TurnLeft_rotates_anticlockwise(Orientation from, Orientation expected)
        => Assert.Equal(expected, from.TurnLeft());

    [Theory]
    [InlineData(Orientation.North, Orientation.East)]
    [InlineData(Orientation.East, Orientation.South)]
    [InlineData(Orientation.South, Orientation.West)]
    [InlineData(Orientation.West, Orientation.North)]
    public void TurnRight_rotates_clockwise(Orientation from, Orientation expected)
        => Assert.Equal(expected, from.TurnRight());

    [Fact]
    public void Four_left_turns_return_to_start()
    {
        var result = Orientation.North.TurnLeft().TurnLeft().TurnLeft().TurnLeft();
        Assert.Equal(Orientation.North, result);
    }

    [Theory]
    [InlineData(Orientation.North, 0, 1)]
    [InlineData(Orientation.East, 1, 0)]
    [InlineData(Orientation.South, 0, -1)]
    [InlineData(Orientation.West, -1, 0)]
    public void ToDelta_returns_unit_step(Orientation orientation, int dx, int dy)
        => Assert.Equal((dx, dy), orientation.ToDelta());

    [Theory]
    [InlineData(Orientation.North, 'N')]
    [InlineData(Orientation.East, 'E')]
    [InlineData(Orientation.South, 'S')]
    [InlineData(Orientation.West, 'W')]
    public void ToSymbol_and_FromSymbol_round_trip(Orientation orientation, char symbol)
    {
        Assert.Equal(symbol, orientation.ToSymbol());
        Assert.Equal(orientation, OrientationExtensions.FromSymbol(symbol));
    }

    [Fact]
    public void FromSymbol_is_case_insensitive()
        => Assert.Equal(Orientation.North, OrientationExtensions.FromSymbol('n'));

    [Fact]
    public void FromSymbol_throws_on_unknown_symbol()
        => Assert.Throws<FormatException>(() => OrientationExtensions.FromSymbol('X'));
}

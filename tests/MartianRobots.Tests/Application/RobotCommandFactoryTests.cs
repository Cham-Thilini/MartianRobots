using MartianRobots.Application;
using MartianRobots.Domain;
using MartianRobots.Domain.Commands;

namespace MartianRobots.Tests.Application;

public class RobotCommandFactoryTests
{
    private readonly IRobotCommandFactory _factory = RobotCommandFactory.CreateDefault();

    [Theory]
    [InlineData('L', typeof(TurnLeftCommand))]
    [InlineData('R', typeof(TurnRightCommand))]
    [InlineData('F', typeof(MoveForwardCommand))]
    public void Create_resolves_the_standard_commands(char token, Type expected)
        => Assert.IsType(expected, _factory.Create(token));

    [Fact]
    public void Create_is_case_insensitive()
        => Assert.IsType<MoveForwardCommand>(_factory.Create('f'));

    [Fact]
    public void Create_throws_on_an_unknown_token()
        => Assert.Throws<FormatException>(() => _factory.Create('Z'));

    [Fact]
    public void A_new_command_type_can_be_registered_without_changing_existing_code()
    {
        // Demonstrates the extensibility the brief asks for: a brand-new command 'B'
        // wired in purely by adding a dictionary entry.
        var factory = new RobotCommandFactory(new Dictionary<char, IRobotCommand>
        {
            ['B'] = new NoOpCommand(),
        });

        Assert.IsType<NoOpCommand>(factory.Create('B'));
    }

    private sealed class NoOpCommand : IRobotCommand
    {
        public RobotState Execute(RobotState state, IMarsWorld world) => state;
    }
}

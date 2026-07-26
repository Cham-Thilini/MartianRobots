using MartianRobots.Domain.Commands;

namespace MartianRobots.Application;

/// <summary>
/// Maps instruction characters to command instances. The map is injected, so supporting a
/// new command type is purely additive: implement <see cref="IRobotCommand"/> and add an
/// entry — nothing in the parser or simulator changes.
/// Commands are stateless, so a single shared instance per token is safe.
/// </summary>
public sealed class RobotCommandFactory : IRobotCommandFactory
{
    private readonly IReadOnlyDictionary<char, IRobotCommand> _commands;

    public RobotCommandFactory(IReadOnlyDictionary<char, IRobotCommand> commands)
        => _commands = commands ?? throw new ArgumentNullException(nameof(commands));

    /// <summary>The standard L / R / F command set from the brief.</summary>
    public static RobotCommandFactory CreateDefault() => new(new Dictionary<char, IRobotCommand>
    {
        ['L'] = new TurnLeftCommand(),
        ['R'] = new TurnRightCommand(),
        ['F'] = new MoveForwardCommand(),
    });

    public IRobotCommand Create(char token)
    {
        if (_commands.TryGetValue(char.ToUpperInvariant(token), out var command))
            return command;

        throw new FormatException($"Unknown robot command '{token}'.");
    }
}

using MartianRobots.Domain;

namespace MartianRobots.Application;

/// <summary>
/// The application entry point / use-case: text in, text out. It wires the parser, the
/// per-robot simulator (over a single shared world so scents persist between robots), and
/// the formatter. The console layer only has to hand it a string.
/// </summary>
public sealed class SimulationRunner
{
    private readonly IMissionParser _parser;
    private readonly IRobotCommandFactory _commandFactory;
    private readonly IResultFormatter _formatter;

    public SimulationRunner(
        IMissionParser parser,
        IRobotCommandFactory commandFactory,
        IResultFormatter formatter)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
    }

    /// <summary>Convenience factory that wires up the standard collaborators.</summary>
    public static SimulationRunner CreateDefault() => new(
        new MissionParser(),
        RobotCommandFactory.CreateDefault(),
        new ResultFormatter());

    public string Run(string input)
    {
        var parsed = _parser.Parse(input);
        var world = new MarsWorld(parsed.Grid);
        var simulator = new RobotSimulator(_commandFactory);

        var results = parsed.Missions
            .Select(mission => simulator.Run(mission, world))
            .ToList();

        return _formatter.Format(results);
    }
}
